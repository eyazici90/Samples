﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Identity.API.Extensions
{
    public static class SecurityKeyExtension
    {
        public static SymmetricSecurityKey GetSigningKey(string secretKey) =>
         new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
    }
}
