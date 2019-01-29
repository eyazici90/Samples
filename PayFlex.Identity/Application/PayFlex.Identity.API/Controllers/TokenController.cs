using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Galaxy.Domain.Auditing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PayFlex.Identity.API.Extensions;
using PayFlex.Identity.Application.Contracts.Services;
using PayFlex.Identity.Shared;
using PayFlex.Identity.Shared.Dtos.User;
using PayFlex.Identity.Shared.Requests;
using PayFlex.Identity.Shared.Responses;

namespace PayFlex.Identity.API.Controllers
{
    [Route("/")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITenanAppService _tenantAppService;
        private readonly IUserAppService _userAppService;
        public TokenController(IUserAppService userAppService
            , ITenanAppService tenantAppServ)
        {
            _userAppService = userAppService ?? throw new ArgumentNullException(nameof(userAppService));
            _tenantAppService = tenantAppServ ?? throw new ArgumentNullException(nameof(tenantAppServ));
        }

        [Route("/api/v1/token")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<GetTokenResponse> Post([FromBody] GetTokenRequest request)
        {
            var claimList = (await IsValid(new UserCredantialsDto { Username = request.Username, Password = request.Password, TenantId = request.TenantId }));

            if (claimList.Any())
                return Generate(request.Username, claimList.ToList());
            
            throw new Exception($"Invalid username or password for {request.Username}");
        }

        [NonAction]
        private async Task<IList<Claim>> IsValid(UserCredantialsDto credentials)
        {
            IList<Claim> _claimList = new List<Claim>();

            var IsValidUser = await _userAppService.ValidateCredentialsByUserName(credentials);

            if (IsValidUser)
            {
                var user = await this._userAppService.FindByUsernameAsync(credentials.Username);

                _claimList.Add(new Claim(ClaimTypes.UserData, user.Id.ToString()));

                var userTenants = await this._userAppService.GetUserTenantsByUserId(user.Id);

                if (credentials.TenantId.HasValue)
                    return _claimList;
                
                var tenant = await _tenantAppService.GetTenantByIdAsync(credentials.TenantId.Value);

                var userTenant = userTenants.SingleOrDefault(t => t.TenantId == tenant.Id);

                if (userTenants.Any(t=>t.TenantId == userTenant.TenantId))
                    _claimList.Add(new Claim(nameof(IMultiTenant.TenantId), userTenant.TenantId.ToString()));
            }

            return _claimList; 
        }


        [NonAction]
        private GetTokenResponse Generate(string username, List<Claim> claimList)
        {
            var dtExpired = new DateTimeOffset(DateTime.Now.AddMinutes(180));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, dtExpired.ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, $"IdentityIssuer"),
                new Claim(JwtRegisteredClaimNames.Aud, $"IdentityAudience")
            };

            if (claimList != null || claimList.Any())
                claims.AddRange(claimList);


            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(SecurityKeyExtension.GetSigningKey(Settings.API_SECRET)
                                            , SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims)); 

            return new GetTokenResponse
            {
                ExpiredDate = dtExpired,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

    }
}
