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
        IEnumerable<User> GetAllUsersINFOAsync(int page , int Records_count);
        User GetUserINFOAsync(User userModel);

        string FindUsername(UserDTO userModel);

        void AddUserAsync(UserDTO userModel);




    }
}
