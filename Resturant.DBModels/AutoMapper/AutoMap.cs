using AutoMapper;
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

            CreateMap<Role, RoleDTO>();
            CreateMap<RoleDTO, Role>();

            CreateMap<Food, FoodDTO>();
            CreateMap<FoodDTO, Food>();







        }
    }
}
