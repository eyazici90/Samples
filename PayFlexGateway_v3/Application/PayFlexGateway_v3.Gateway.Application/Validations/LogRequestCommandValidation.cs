
using FluentValidation;
using PayFlexGateway_v3.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlexGateway_v3.Gateway.Application.Validations
{
    public class LogRequestCommandValidation : AbstractValidator<LogRequestCommand>
    {
        public LogRequestCommandValidation()
        {
            RuleFor(t => t.Body).NotEmpty().MinimumLength(2);
            RuleFor(t => t.Url).NotEmpty().MinimumLength(2);
        }
    }

    public class LogResponseCommandValidation : AbstractValidator<LogResponseCommand>
    {
        public LogResponseCommandValidation()
        {
            RuleFor(t => t.Body).NotEmpty().MinimumLength(2);
            RuleFor(t => t.Url).NotEmpty().MinimumLength(2);
        }
    }
}
