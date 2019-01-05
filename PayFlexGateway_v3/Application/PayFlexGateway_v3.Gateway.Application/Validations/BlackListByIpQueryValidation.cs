using FluentValidation;
using PayFlexGateway_v3.Gateway.Shared.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlexGateway_v3.Gateway.Application.Validations
{ 
    public class BlackListByIpQueryValidation : AbstractValidator<BlackListByIpQuery>
    {
        public BlackListByIpQueryValidation()
        {
            RuleFor(t => t.Ip)
                .NotEmpty().NotNull();
        }
    } 
}
