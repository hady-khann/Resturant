
using Resturant.DataAccess.Context;
using Resturant.Repository.Interfaces;
using Resturant.Repository;
using Resturant.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant.Repository.Implements;
using AutoMapper;

namespace Resturant.Repository.UOW
{
    public class UOW : IUOW
    {
        public ResturantContext _context { get; }
        private readonly IMapper _Mapper;
        private IUser_Repo _UserRep;
        private IRole_Repo _RoleRep;
        private IUserInfo_Repo _UserInfoRep;

        public UOW(ResturantContext context,IMapper Mapper)
        {
            this._context = context;
            _Mapper = Mapper;
        }


        public IBase_Repo<TEntity> _Base<TEntity>() where TEntity : class
        {
            IBase_Repo<TEntity> repository = new Base_Repo<TEntity, ResturantContext>(_context);
            return  repository;
        }


        public IUser_Repo _User
        {
            get
            {
                if (_UserRep == null)
                {
                    _UserRep = new User_Repo(_context,_Mapper);
                }

                return _UserRep;
            }
        }

        public IRole_Repo _Role
        {
            get
            {
                if (_RoleRep == null)
                {
                    _RoleRep = new Role_Repo(_context);
                }

                return _RoleRep;
            }
        }

        public IUserInfo_Repo _UserInfo
        {
            get
            {
                if (_UserInfoRep == null)
                {
                    _UserInfoRep = new UserInfo_Repo(_context,_Mapper);
                }

                return _UserInfoRep;
            }
        }


        public async Task SaveDBAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SaveDB()
        {
            _context.SaveChanges();
        }
    }
}
