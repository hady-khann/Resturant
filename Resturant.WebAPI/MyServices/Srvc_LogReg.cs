using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Resturant.Core.Models;
using Resturant.Infrastructure.Auth.AuthJWT;
using Resturant.Infrastructure.DTO.Auth;
using Resturant.Infrastructure.Repository.UserRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.WebAPI.MyServices
{
    public class Srvc_LogReg
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public Srvc_LogReg(IConfiguration config, ITokenService tokenService, IUserRepository userRepository)
        {
            _config = config;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }



        public string Login(User user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.PassWord))
            {
                return "Please Enter UserName And PassWord Empty";
            }

            var validUser = _userRepository.GetUser(user);

            if (validUser != null)
            {
                var generatedToken = _tokenService.AuthenticateUser(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), validUser);

                if (generatedToken != null)
                {
                    return generatedToken;
                }
                else
                {
                    return "Wrong Username and Password";
                }
            }
            else
            {
                return "User doesn't exists ";
            }

        }



    }
}
