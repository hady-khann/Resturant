using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Resturant.DataAccess.Context;
using Resturant.DBModels.DTO;
using Resturant.DBModels.DTO.Auth;
using Resturant.DBModels.Entities;
using Resturant.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





namespace Resturant.Repository
{
    public class User_Repo : IUser_Repo
    {
        private readonly ResturantContext _context;
        private readonly IMapper _Mapper;

        public User_Repo(ResturantContext context, IMapper mapper)
        {
            _context = context;
            _Mapper = mapper;
        }


    }
}
