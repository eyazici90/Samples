using FluentAssertions;
using PayFlex.Identity.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PayFlex.Identity.Domain.Tests.ExceptionTests
{
    public class Exception_Tests
    {
        [Fact]
        public void Fire_new_identity_domain_exception_success()
        {
            var fakeException = new IdentityDomainException();

            fakeException.Should().NotBeNull();
        }

        [Fact]
        public void Fire_new_identity_domain_exception_with_messesage_success()
        {
            var fakeExceptionMessage = "Test exception";

            var fakeException = new IdentityDomainException(fakeExceptionMessage);

            fakeException.Should().NotBeNull();
        }


        [Fact]
        public void Fire_new_identity_domain_exception_with_messesage_and_inner_exception_success()
        {
            var fakeExceptionMessage = "Test exception";

            var fakeInnerException = new Exception(fakeExceptionMessage);

            var fakeException = new IdentityDomainException(fakeExceptionMessage, fakeInnerException);

            fakeException.Should().NotBeNull();
        }
    }
}
