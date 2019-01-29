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
    public class ObjectMapperTests : PayFlexIdentityTestApplication
    {
        private readonly IObjectMapper _objectMapper;
        public ObjectMapperTests()
        {
            _objectMapper = TheObject<IObjectMapper>();
        }

        [Fact]
        public void Object_mapper_should_map_user_to_user_dto_success()
        {
            var fakeTenantId = 1;

            var user = User.Create("emre.yazici");

            var result = this._objectMapper.MapTo<UserDto>(user);

            result.UserName.Should().BeSameAs(user.UserName);
            result.Id.Should().Be(user.Id);
        }

        [Fact]
        public void Object_mapper_should_map_user_to_multiple_user_dto_success()
        {
            var fakeTenantId = 1;

            var user = User.Create("emre.yazici");

            var result = this._objectMapper.MapTo<UserDto>(user);

            result.UserName.Should().BeSameAs(user.UserName);
            result.Id.Should().Be(user.Id);

            var secondResult = this._objectMapper.MapTo<UserDto>(user);

            secondResult.UserName.Should().BeSameAs(user.UserName);
            secondResult.Id.Should().Be(user.Id);

        }
    }
}
