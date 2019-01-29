using FluentAssertions;
using Galaxy.Repositories;
using Microsoft.EntityFrameworkCore;
using PayFlex.Identity.Domain.AggregatesModel.UserAggregate;
using PayFlex.Identity.Shared.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PayFlex.Identity.Application.Tests.Repositories
{
    public class UserRepositoryTests : PayFlexIdentityTestApplication
    {
        private readonly IRepositoryAsync<User> _userRep;
        private readonly IRepositoryAsync<User, int> _userRepWithKey;
        public UserRepositoryTests()
        {
            _userRep = TheObject<IRepositoryAsync<User>>();
            _userRepWithKey = TheObject<IRepositoryAsync<User, int>>();
        }

        [Fact]
        public void Resolving_repository_from_container_success()
        {
            _userRep.Should().NotBeNull();
        }

        [Fact]
        public void Resolving_repository_with_key_from_container_success()
        {
            _userRep.Should().NotBeNull();
        }

        [Fact]
        public async Task User_repository_insert_user_success()
        {
            var fakeTenantId = 1;

            var fakeUser = User.Create("test-user-1");

            Func<Task> act = async () => await _userRep.InsertAsync(fakeUser);

            await act.Should().NotThrowAsync<Exception>();
          
        } 

        [Fact]
        public async Task User_repository_get_existing_user_by_id_success()
        {
            var fakeUserId = 1;

            Func<Task> act = async () => await _userRep.FindAsync(fakeUserId);

            await act.Should().NotThrowAsync<Exception>();
        }

        [Fact]
        public async Task User_repository_get_existing_all_users_success()
        { 
            Func<Task> act = async () => await _userRep.Queryable()
                                                            .ToListAsync();

            await act.Should().NotThrowAsync<Exception>();
        }

    }
}
