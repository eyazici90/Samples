using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Domain.Exceptions
{
    public class IdentityDomainException : Exception
    {
        public IdentityDomainException()
        { }

        public IdentityDomainException(string message)
            : base(message)
        { }

        public IdentityDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
