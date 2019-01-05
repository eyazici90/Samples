using FluentValidation;
using PayFlexGateway_v3.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlexGateway_v3.Gateway.Application.Validations
{ 
    public class AddIPToBlacklistCommandValidation : AbstractValidator<AddIPToBlacklistCommand>
    {
        public AddIPToBlacklistCommandValidation()
        {
            RuleFor(t => t.ClientIp)
                .NotEmpty().NotNull();
        }
    }
}
