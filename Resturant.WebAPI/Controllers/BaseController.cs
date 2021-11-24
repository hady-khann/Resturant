using Microsoft.AspNetCore.Mvc;
using Resturant.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resturant.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        internal Global_Response_DTO<T> Global_Controller_Result<T>(T data, string message,bool Success)
        {
            try
            {
                return new Global_Response_DTO<T>
                {
                    Data = data,
                    Message = message,
                    Success = true
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
