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
        public string Role { get; set; }
        public int? Level { get; set; }


        //#region Explicit
     
        //public static explicit operator UserDTO (User model)
        //{
        //    if (model == null)
        //    {
        //        return null;
        //    }
        //    return new UserDTO
        //    {
        //        Id = model.Id,
        //        RoleId = model.RoleId,
        //        UserName = model.UserName,
        //        Email = model.Email,
        //        Password = model.PassWord,
        //        Status = model.Status,
        //        Role=model.Role.RoleName,
        //        Level=model.Role.AccessLevel,
        //    };
        //}
        //#endregion
    }

    //public class UserAccount
    //{
    //    public Guid Id { get; set; }
    //    public string Email { get; set; }
    //    public string UserName { get; set; }
    //}

}
