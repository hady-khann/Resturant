using System;
using System.Collections.Generic;

#nullable disable

namespace Resturant.Core.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public int AccessLevel { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
