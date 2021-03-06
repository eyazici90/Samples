﻿using Galaxy.Infrastructure;
using Galaxy.Session;
using System;

namespace Galaxy.DataContext
{
    public interface IGalaxyContext : IDisposable
    {
        int SaveChanges();
        void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState;
        void SyncEntityState<TEntity>(TEntity entity) where TEntity : class, IObjectState;
        void SyncObjectsStatePostCommit();
        void SyncObjectsAuditPreCommit(IAppSessionBase session);
        bool CheckIfThereIsAvailableTransaction();
    }
}
