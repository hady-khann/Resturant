﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Resturant.DataAccess
{
    public partial class UsersInfo
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public int Wallet { get; set; }
        public string Address { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
