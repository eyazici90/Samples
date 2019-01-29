using Galaxy.Session;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PayFlex.Identity.API.Session
{ 
    public class PayFlexIdentityAppSession : IAppSessionContext
    {
        public IHttpContextAccessor _httpContextAccessor;
        public PayFlexIdentityAppSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public  int? TenantId
        {
            get
            {
                try
                {
                    return Convert.ToInt32(this._httpContextAccessor.HttpContext.User.Claims?
                        .FirstOrDefault(x => x.Type == nameof(IAppSessionContext.TenantId)).Value);
                }
                catch { return null; }
            }
            set => TenantId = value;
        }

        public  int? UserId
        {
            get => Convert.ToInt32(this._httpContextAccessor.HttpContext.User.Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.UserData).Value);
            set => UserId = value;
        } 

        public int? GetCurrenTenantId() => TenantId;
        
    }
}
