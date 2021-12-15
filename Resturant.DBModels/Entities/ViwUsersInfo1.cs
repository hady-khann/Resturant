using System;
using System.Collections.Generic;

#nullable disable

namespace Resturant.DBModels.Entities
{ 
    public partial class ViwUsersInfo
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public int Wallet { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public int AccessLevel { get; set; }
    }
}
