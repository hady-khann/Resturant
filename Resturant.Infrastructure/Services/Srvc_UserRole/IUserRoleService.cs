using Microsoft.EntityFrameworkCore;
using Resturant.Core.Models;
using Resturant.Infrastructure.DTO.Auth;
using Resturant.Infrastructure.Repository.User_Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Infrastructure.Services.Srvs_UserRole
{
    public interface IUserRoleService
    {
        UserDTO GetUserRoleDTO(User usermodel);
    }
}
