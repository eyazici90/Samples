using FluentValidation;
using PayFlex.Identity.Shared.Dtos.Tenant;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Application.Validations
{
    public class TenantDtoValidation : AbstractValidator<TenantDto>
    {
        public TenantDtoValidation()
        {
            RuleFor(t => t.Name)
                .NotEmpty().MinimumLength(3);
        }
    }
}
