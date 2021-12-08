﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Resturant.DBModels.Entities
{
    public partial class User
    {
        public User()
        {
            UserOrders = new HashSet<UserOrder>();
        }

        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public int Wallet { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<UserOrder> UserOrders { get; set; }

        [NotMapped]
        public string? RoleName { get; set; }
    }
}
