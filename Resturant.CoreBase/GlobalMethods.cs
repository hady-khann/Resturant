using Microsoft.AspNetCore.Http;
using Resturant.DBModels.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.CoreBase.Global_Methods

{
    public class GlobalMethods
    {
        private readonly IHttpContextAccessor _ContextAccessor;

        public GlobalMethods(IHttpContextAccessor contextAccessor)
        {
            _ContextAccessor = contextAccessor;
        }

        public AuthDTO GETCurrentUser() => _ContextAccessor.HttpContext.Items["ViwUserInfoDTO"] as AuthDTO;
    }
}
