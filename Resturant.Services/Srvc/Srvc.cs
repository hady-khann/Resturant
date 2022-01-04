using AutoMapper;
using Resturant.DataAccess.Context;
using Resturant.Services.Srvc_Internal.Auth.JWT;
using Resturant.Services.Srvc_Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Services.Srvc
{
    public class Srvc : ISrvc
    {
        public ResturantContext _context { get; }
        private readonly IMapper _Mapper;
        private ISrvc_User _SrvcUser;
        private ISrvc_Token _SrvcToken;


        public ISrvc_User _User
        {
            get
            {
                if (_SrvcUser == null)
                {
                    _SrvcUser = new Srvc_User(_context, _Mapper);
                }

                return _SrvcUser;
            }
        }

        public ISrvc_Token _Token
        {
            get
            {
                if (_SrvcToken == null)
                {
                    _SrvcToken = new Srvc_Token();
                }

                return _SrvcToken;
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
