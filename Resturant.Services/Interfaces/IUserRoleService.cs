using Microsoft.EntityFrameworkCore;
using Resturant.DBModels.DTO.Auth;
using Resturant.DBModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Services.Interfaces
{
    public interface IUserRoleService
    {
        UserDTO GetUserRoleDTO(User usermodel);
    }
}
