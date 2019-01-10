using FluentValidation;
using PayFlex.Identity.Shared.Dtos.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Application.Validations
{ 
    public class PermissionDtoValidation : AbstractValidator<PermissionDto>
    {
        public PermissionDtoValidation()
        {
            RuleFor(t => t.Name)
                .NotEmpty().MinimumLength(3);
        }
    }
}
