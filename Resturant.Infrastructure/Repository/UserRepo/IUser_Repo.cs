using Resturant.Infrastructure.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant.Core.Models;

namespace Resturant.Infrastructure.Repository.User_Repo
{
    public interface IUser_Repo
    {
        IEnumerable<User> GetAllUsersINFO(int page , int Records_count);
        User GetUserINFO(User userModel);


    }
}
