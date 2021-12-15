using System;
using System.Collections.Generic;

#nullable disable

namespace Resturant.DBModels.DTO
{
    public partial class RoleDTO
    {

        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public int AccessLevel { get; set; }
        public string Description { get; set; }
    }
}
