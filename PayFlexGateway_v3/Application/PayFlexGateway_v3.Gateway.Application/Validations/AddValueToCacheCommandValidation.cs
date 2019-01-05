using FluentValidation;
using PayFlexGateway_v3.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlexGateway_v3.Gateway.Application.Validations
{ 
    public class AddValueToCacheCommandValidation : AbstractValidator<AddValueToCacheCommand>
    {
        public AddValueToCacheCommandValidation()
        {
            RuleFor(t => t.CacheKey)
                .NotEmpty();

            RuleFor(t => t.CacheValue)
                .NotEmpty();
        }
    }
}
