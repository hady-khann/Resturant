using AutoMapper;
using Resturant.Core.Models;
using Resturant.Infrastructure.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Infrastructure.DTO
{
    class AutoMap : Profile
    {
        public AutoMap()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
