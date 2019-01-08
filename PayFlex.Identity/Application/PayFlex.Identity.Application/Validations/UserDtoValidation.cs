using FluentValidation;
using PayFlex.Identity.Shared.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Application.Validations
{
    public class UserDtoValidation : AbstractValidator<UserDto>
    {
        public UserDtoValidation()
        {
            RuleFor(t => t.UserName).NotEmpty().MinimumLength(5);
        }
    }
}
