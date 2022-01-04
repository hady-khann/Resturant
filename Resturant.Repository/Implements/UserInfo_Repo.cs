using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Resturant.DataAccess.Context;
using Resturant.DBModels.DTO;
using Resturant.DBModels.Entities;
using Resturant.Repository.Interfaces;

namespace Resturant.Repository.Implements
{
    public class UserInfo_Repo : IUserInfo_Repo
    {


        private readonly ResturantContext _context;
        private readonly IMapper _Mapper;

        public UserInfo_Repo(ResturantContext context, IMapper mapper)
        {
            _context = context;
            _Mapper = mapper;
        }

    }
}

