using Resturant.Core.Models;
using System;
using System.Text.Json.Serialization;

namespace Resturant.Infrastructure.DTO.Auth
{

    public class UserDTO
    {
        [JsonIgnore]
        public Guid UserID { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }


        #region Explicit
        public static explicit operator User(UserDTO model)
        {
            if (model == null)
            {
                return null;
            }
            return new User
            {
                Id=model.UserID,
                UserName = model.UserName,
                PassWord = model.Password,
                Email = model.Email,
                Wallet = 0,
                Status = true,
                RoleId = Guid.Parse(model.Role),

            };
        }

        public static explicit operator UserDTO (User model)
        {
            if (model == null)
            {
                return null;
            }
            return new UserDTO
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = model.PassWord,
                Role=model.Role.RoleName,
            };
        }
        #endregion
    }
}
