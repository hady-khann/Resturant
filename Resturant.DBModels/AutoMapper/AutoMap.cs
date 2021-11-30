using AutoMapper;
using Resturant.DBModels.Entities;
using Resturant.DBModels.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.DBModels.AutoMapper
{
    class AutoMap : Profile
    {
        public AutoMap()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
