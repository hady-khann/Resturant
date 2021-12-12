using AutoMapper;
using Resturant.DBModels.Entities;
using Resturant.DBModels.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant.DBModels.DTO;

namespace Resturant.DBModels.AutoMapper
{
    class AutoMap : Profile
    {
        public AutoMap()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<UsersInfo, UserInfoDTO>();
            CreateMap<UserInfoDTO, UsersInfo>();

        }
    }
}
