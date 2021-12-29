using Resturant.DBModels.Entities;
using System;
using System.Text.Json.Serialization;

namespace Resturant.DBModels.DTO.Auth
{

    public class UserDTO
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
    }


}
