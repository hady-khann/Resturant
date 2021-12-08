﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant.DBModels.DTO;
using Resturant.DBModels.DTO.Auth;
using Resturant.DBModels.Entities;

namespace Resturant.Repository.Interfaces
{
    public interface IUser_Repo
    {
        IEnumerable<User> GetAllUsersINFO(PaginationDTO pagination, int Level);
        User CheckUserPass(UserDTO userModel);
        string IsUserExists(UserDTO userModel);
        Task AddUserAsync(UserDTO userModel);

    }
}
