using FluentValidation;
using PayFlexGateway_v3.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlexGateway_v3.Gateway.Application.Validations
{
     
    public class LogExceptionByRequestCommandValidation : AbstractValidator<LogExceptionByRequestCommand>
    {
        public LogExceptionByRequestCommandValidation()
        {
            RuleFor(t => t.CreatedException).NotNull();
        }
    }

}
