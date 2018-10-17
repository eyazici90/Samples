using Galaxy.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerSample.API.Host.Session
{
    public class CustomerSampleAppSession : SessionBase
    {
       
        public override int? TenantId { get => 1 ; set => TenantId =  value; }
        public override int? UserId { get => 1; set => UserId = value; }

     

        public override int? GetCurrenTenantId()
        {
            return base.GetCurrenTenantId();
        }

        public override int? GetCurrentUserId()
        {
            return base.GetCurrentUserId();
        }

    

    }
}
