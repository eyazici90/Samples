using FluentValidation;
using PayFlex.Identity.Shared.Dtos.Role;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Application.Validations
{ 
    public class RoleDtoValidation : AbstractValidator<RoleDto>
    {
        public RoleDtoValidation()
        {
            RuleFor(t => t.Name).NotEmpty().MinimumLength(3);
        }
    }
}
