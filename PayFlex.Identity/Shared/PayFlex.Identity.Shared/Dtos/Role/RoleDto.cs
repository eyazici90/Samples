using Galaxy.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Shared.Dtos.Role
{
    public class RoleDto : IEntityDto<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
