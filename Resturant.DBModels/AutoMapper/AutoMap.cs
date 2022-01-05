using AutoMapper;
using Resturant.DBModels.Entities;
using Resturant.DBModels.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant.DBModels.DTO;
using resturant = Resturant.DBModels.Entities.Resturant;


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

            CreateMap<FoodType, FoodTypeDTO>();
            CreateMap<FoodTypeDTO, FoodType>();

            CreateMap<User, ViwUsersInfo>();
            CreateMap<ViwUsersInfo, User>();

            CreateMap<User, ResturantDTO>();
            CreateMap<ResturantDTO, User>();

            CreateMap<ViwUsersInfo, resturant>()
                .ForMember(dest => dest.UserId, op => op.MapFrom(src => src.Id));
            CreateMap<resturant, ViwUsersInfo>();


            CreateMap<ViwResturant, resturant>()
                .ForMember(dest => dest.ResturantType, op => op.MapFrom(src => src.ResturantType));
            CreateMap<resturant, ViwResturant>();





















        }
    }
}
