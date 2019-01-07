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

        [EnableUnitOfWork]
        Task<TenantDto> AddTenant(TenantDto tenant);

        [EnableUnitOfWork]
        Task DeleteTenant(int id);

        [EnableUnitOfWork]
        Task UpdateTenant(TenantDto tenant);
    }
}
