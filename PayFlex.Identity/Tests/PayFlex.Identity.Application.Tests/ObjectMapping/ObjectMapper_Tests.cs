using FluentAssertions;
using Galaxy.ObjectMapping;
using PayFlex.Identity.Domain.AggregatesModel.UserAggregate;
using PayFlex.Identity.Shared.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PayFlex.Identity.Application.Tests.ObjectMapping
{
    public class ObjectMapper_Tests : PayFlexIdentityTestApplication
    {
        private readonly IObjectMapper _objectMapper;
        public ObjectMapper_Tests()
        {
            _objectMapper = TheObject<IObjectMapper>();
        }

        [Fact]
        public void Object_mapper_should_map_user_to_user_dto_success()
        {
            var fakeTenantId = 1;

            var user = User.Create("emre.yazici", fakeTenantId);

            var result = this._objectMapper.MapTo<UserDto>(user);

            result.UserName.Should().BeSameAs(user.UserName);
            result.TenantId.Should().Be(user.TenantId);
            result.Id.Should().Be(user.Id);
        }

        [Fact]
        public void Object_mapper_should_map_user_to_multiple_user_dto_success()
        {
            var fakeTenantId = 1;

            var user = User.Create("emre.yazici", fakeTenantId);

            var result = this._objectMapper.MapTo<UserDto>(user);

            result.UserName.Should().BeSameAs(user.UserName);
            result.TenantId.Should().Be(user.TenantId);
            result.Id.Should().Be(user.Id);

            var secondResult = this._objectMapper.MapTo<UserDto>(user);

            secondResult.UserName.Should().BeSameAs(user.UserName);
            secondResult.TenantId.Should().Be(user.TenantId);
            secondResult.Id.Should().Be(user.Id);

        }
    }
}
