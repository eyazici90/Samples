﻿using CustomerSample.Common.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerSample.Application.Validators
{
    public class BrandValidation : AbstractValidator<BrandDto>
    {
        public BrandValidation()
        {
            RuleFor(t => t.BrandName).NotEmpty();
        }
    }
}
