﻿using AutoMapper;
using Resturant.DBModels.Entities;
using Resturant.DBModels.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant.DBModels.DTO;

namespace Resturant.DBModels.AutoMaping
{
    public class AutoMaping : Profile
    {
        public AutoMaping()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<Viw_UsersInfo, UserInfoDTO>();
            CreateMap<UserInfoDTO, Viw_UsersInfo>();

            CreateMap<UserInfoDTO, UserDTO>(); 
            CreateMap<UserInfoDTO, User>();




        }
    }
}
