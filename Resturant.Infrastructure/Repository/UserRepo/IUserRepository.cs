using Resturant.Infrastructure.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant.Core.Models;

namespace Resturant.Infrastructure.Repository.UserRepo
{
    public interface IUserRepository
    {
        UserDTO GetUser(User userMode);
    }
}
