using Galaxy.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Shared.Dtos.Permission
{
    public class PermissionDto  
    {
        public int Id { get; set; }

        public string Name { get;  set; }

        public string Description { get;  set; }

        public string Url { get;  set; }

        public string Namespace { get;  set; }

        public string ServiceName { get;  set; }

        public string MethodExecutionName { get;  set; }
    }
}
