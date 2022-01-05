using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.DBModels.DTO.Auth
{
    public class AuthDTO
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public string Role { get; set; }
        public int? Level { get; set; }
        public Guid? ResturantId { get; set; }
#nullable enable
        public String? ResType { get; set; }
    }
}
