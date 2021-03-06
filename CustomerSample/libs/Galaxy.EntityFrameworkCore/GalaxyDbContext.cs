using System;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MediatR;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;
using Galaxy.DataContext;
using Galaxy.Session;
using Galaxy.Auditing;
using Galaxy.Infrastructure;
using Galaxy.Domain;
using Galaxy.EFCore.Extensions;
using Galaxy.EntityFrameworkCore;

namespace Galaxy.EFCore
{
    public abstract class GalaxyDbContext: DbContext, IGalaxyContextAsync
    {
        #region Private Fields
        private readonly Guid _instanceId;
        private readonly IAppSessionBase _appSession;
        bool _disposed;
        #endregion Private Fields

        protected static MethodInfo ConfigureGlobalFiltersMethodInfo = typeof(GalaxyDbContext).GetMethod(nameof(ConfigureGlobalFilters)
            , BindingFlags.Instance | BindingFlags.NonPublic);

     

        public GalaxyDbContext(DbContextOptions options) : base(options)
        {
            _instanceId = Guid.NewGuid();
        }

        public GalaxyDbContext(DbContextOptions options, IAppSessionBase appSession) : base(options)
        {
            _instanceId = Guid.NewGuid();

            this._appSession = appSession ?? throw new ArgumentNullException(nameof(appSession));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                ConfigureGlobalFiltersMethodInfo
                  .MakeGenericMethod(entityType.ClrType)
                   .Invoke(this, new object[] { entityType, modelBuilder });
            }
        }


        public Guid InstanceId { get { return _instanceId; } }


        protected virtual void ConfigureGlobalFilters<TEntity>(IMutableEntityType entityType, ModelBuilder modelBuilder)
           where TEntity : class
        {
            if (entityType.BaseType == null && ShouldFilterEntity<TEntity>(entityType))
            {
                var filterExpression = CreateFilterExpression<TEntity>();
                if (filterExpression != null)
                {
                    modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
                }
            }
        }

        protected virtual Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
          where TEntity : class
        {
            Expression<Func<TEntity, bool>> expression = null;
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> softDeleteFilter = e => !((ISoftDelete)e).IsDeleted ;
                expression = expression == null ? softDeleteFilter : CombineExpressions(expression, softDeleteFilter);
            }
            if (typeof(IFullyAudit).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> tenanFilter = e => ((IFullyAudit)e).TenantId == this._appSession.GetCurrenTenantId();
                expression = expression == null ? tenanFilter : CombineExpressions(expression, tenanFilter);
            }
            return expression;
        }
        

        protected virtual bool ShouldFilterEntity<TEntity>(object entityType) where TEntity : class
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                return true;
            }
            if (typeof(IFullyAudit).IsAssignableFrom(typeof(TEntity)))
            {
                return true;
            }
            return false;
        }

        protected virtual Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new GalaxyExpressionVisitor(expression1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expression1.Body);

            var rightVisitor = new GalaxyExpressionVisitor(expression2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expression2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
        }
        
        public override int SaveChanges()
        {
            SyncObjectsStatePreCommit();
            var changes = base.SaveChanges();
            SyncObjectsStatePostCommit();
            return changes;
        }

        public virtual bool CheckIfThereIsAvailableTransaction()
        {
            return !ChangeTracker.Entries().All(e=>e.State == EntityState.Unchanged);
        }

        public virtual int SaveChangesByPassed()
        {
            var changes = base.SaveChanges();
            return changes;
        }

        public virtual async Task<int> SaveChangesByPassedAsync()
        {
            return await base.SaveChangesAsync();
        }

        public virtual async Task<int> SaveChangesByPassedAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await this.SaveChangesAsync(CancellationToken.None);
        }
  
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
             this.SyncObjectsStatePreCommit();
             var changesAsync = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
             this.SyncObjectsStatePostCommit();
            return changesAsync;
        }

        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState
        {
            Entry(entity).State = StateHelper.ConvertState(entity.ObjectState);
        }

        public void SyncEntityState<TEntity>(TEntity entity) where TEntity : class, IObjectState
        {
            entity.SyncObjectState(StateHelper.ConvertState(Entry(entity).State));
        }

        private void SyncObjectsStatePreCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                dbEntityEntry.State = StateHelper.ConvertState(((IObjectState)dbEntityEntry.Entity).ObjectState);
            }
        }

        public void SyncObjectsAuditPreCommit (IAppSessionBase session)
        {
            if (!ChangeTracker.Entries().Any(x => x.Entity is IFullyAudit))
                return;
            foreach (var dbEntityEntry in ChangeTracker.Entries<IFullyAudit>())
            {
                    var entity = ((IFullyAudit)dbEntityEntry.Entity);
                    if (((IObjectState)dbEntityEntry.Entity).ObjectState == ObjectState.Added)
                    {
                       entity.SyncAuditState(creatorUserId : session.UserId,tenantId: session.TenantId, creationTime: DateTime.Now);                       
                    }
                    else
                    {
                       entity.SyncAuditState(lastmodifierUserId: session.UserId,tenantId: session.TenantId, lastModificationTime: DateTime.Now
                            , creatorUserId : entity.CreatorUserId, creationTime : entity.CreationTime);
                    }
            }
        }

        public void SyncObjectsStatePostCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                ((IObjectState)dbEntityEntry.Entity).SyncObjectState(StateHelper.ConvertState(dbEntityEntry.State));
            }
        }

        public async Task DispatchNotificationsAsync(IMediator mediator)
        {
            var notifications = ChangeTracker
                .Entries<IEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = notifications
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            notifications.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);

        }

        protected  void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    
                    // free other managed objects that implement
                    // IDisposable only
                }

                // release any unmanaged objects
                // set object references to null

                _disposed = true;
            }

            Dispose(disposing);
        }

      
    }
}