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

        //public IEnumerable<UserInfoDTO> GetAllUsersINFO(PaginationDTO pagination, int AccessLevel = 5)
        //{
        //    var skip = pagination.PageNumber * pagination.RowNumber;
        //    var take = pagination.RowNumber;

        //    var EFresult = _context.UsersInfos.Where(x => x.AccessLevel >= AccessLevel).Skip(skip).Take(take).ToList();

        //    //IEnumerable<UserInfoDTO> enumerable = EFresult.Select(c => _Mapper.Map<UserInfoDTO>(c));

        //    return _Mapper.Map<IEnumerable<UserInfoDTO>>(EFresult);
        //}

        //public UsersInfo GetUsersINFOByID(Guid Id)
        //{
        //    return _context.UsersInfos.FirstOrDefault(x => x.Id == Id) as UsersInfo; ;
            
        //}
    }
}

