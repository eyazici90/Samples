﻿using Galaxy.Application;
using Galaxy.ObjectMapping;
using Galaxy.Repositories;
using Galaxy.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using PayFlex.Identity.Application.Contracts.Services;
using PayFlex.Identity.Domain.AggregatesModel.RoleAggregate;
using PayFlex.Identity.Shared.Dtos.Role;
using PayFlex.Identity.Shared.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Identity.Application.Services
{
    public class RoleAppService : CrudAppServiceAsync<Role, RoleDto, int>, IRoleAppService
    {
        public RoleAppService(IRepositoryAsync<Role, int> repositoryAsync
            , IUnitOfWorkAsync unitOfWorkAsync
            , IObjectMapper objectMapper) : base(repositoryAsync, unitOfWorkAsync, objectMapper)
        {
        }

        public async Task<RoleDto> AddRoleAsync(RoleDto roleDto)
        {
            return await AddAsync(async () => {
                var role = Role.Create(roleDto.Name);
                return role;
            });
        }

        public  async Task<IList<RoleDto>> GetAllRolesAsync()
        {
            return await QueryableNoTrack().ToListAsync();
        }

        public async Task<RoleDto> GetRoleByIdAsync(int roleId)
        {
            return await FindAsync(roleId);
        }
         

        public async Task UpdateRoleAsync(RoleDto roleDto)
        {
            await UpdateAsync(roleDto.Id, async role => {
                role.ChangeName(roleDto.Name);
            });
        }

        public async Task DeleteRoleAsync(int roleId)
        {
            //Hard Delete
            //await DeleteAsync(roleId);

            //Soft Delete
            await UpdateAsync(roleId, async (role) => {
                role.DeleteThisRole();
            });
        }
    }
}