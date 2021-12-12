using Resturant.DBModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.DBModels.DTO
{
    public class UserInfoDTO
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
