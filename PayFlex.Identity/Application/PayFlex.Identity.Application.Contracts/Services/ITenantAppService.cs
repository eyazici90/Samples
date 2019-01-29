using Galaxy.Application;
using Galaxy.UnitOfWork;
using PayFlex.Identity.Shared.Dtos.Tenant;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Identity.Application.Contracts.Services
{
    public interface ITenanAppService : IApplicationService
    {
        Task<List<TenantDto>> GetAllTenantsAsync();

        Task<TenantDto> GetTenantByIdAsync(int id);

        Task<TenantDto> GetTenantByNameAsync(string tenantName);

        [EnableUnitOfWork]
        Task<TenantDto> AddTenantAsync(TenantDto tenant);

        [EnableUnitOfWork]
        Task DeleteTenantAsync(int id);

        [EnableUnitOfWork]
        Task UpdateTenantAsync(TenantDto tenant);
    }
}
