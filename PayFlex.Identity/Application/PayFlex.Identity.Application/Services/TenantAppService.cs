using Galaxy.Application;
using Galaxy.ObjectMapping;
using Galaxy.Repositories;
using Galaxy.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using PayFlex.Identity.Application.Contracts.Services;
using PayFlex.Identity.Domain.AggregatesModel.TenantAggregate;
using PayFlex.Identity.Shared.Dtos.Tenant;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Identity.Application.Services
{
    public class TenantAppService : CrudAppServiceAsync<TenantDto, int, Tenant>, ITenanAppService
    {
        public TenantAppService(IRepositoryAsync<Tenant, int> repositoryAsync
            , IObjectMapper objectMapper ) : base(repositoryAsync, objectMapper)
        {
        }

        public async Task<TenantDto> AddTenantAsync(TenantDto tenantDto)
        {
            return await AddAsync(async () => {
                var tenant = Tenant.Create(tenantDto.Name, tenantDto.Description);
                return tenant;
            });
        }

        public async Task DeleteTenantAsync(int id)
        {
            await DeleteAsync(id);
        }

        public async Task<List<TenantDto>> GetAllTenantsAsync()
        {
            return await QueryableNoTrack().ToListAsync();
        }

        public async Task<TenantDto> GetTenantByIdAsync(int id)
        {
            return await FindAsync(id);
        }

        public async Task UpdateTenantAsync(TenantDto tenantDto)
        {
            await UpdateAsync(tenantDto.Id, async tenant => {
                tenant.ChangeName(tenantDto.Name);
                tenant.ChangeOrAddDesc(tenant.Description);
            });
        }
    }
}
