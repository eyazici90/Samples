using FluentValidation;
using PayFlex.Identity.Shared.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Application.Validations
{ 
    public class CreateUserRequestValidation : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidation()
        {
            RuleFor(t => t.UserName).NotEmpty().MinimumLength(5);
        }
    }
}
