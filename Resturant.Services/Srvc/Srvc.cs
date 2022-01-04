using AutoMapper;
using Resturant.DataAccess.Context;
using Resturant.Repository.UW;
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
        private ISrvc_UserRes _SrvcUserRes;
        private ISrvc_Token _SrvcToken;
        private _IUW _IUW;

        public Srvc(ResturantContext context, IMapper mapper, _IUW IUW)
        {
            this._context = context;
            _IUW = IUW;
            _Mapper = mapper;
        }

        public ISrvc_UserRes _UserRes
        {
            get
            {
                if (_SrvcUserRes == null)
                {
                    _SrvcUserRes = new Srvc_UserRes(_context, _Mapper, _IUW);
                }

                return _SrvcUserRes;
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
