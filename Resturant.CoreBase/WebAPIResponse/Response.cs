using Resturant.DBModels.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.CoreBase.WebAPIResponse
{
    public class Response
    {
        public Global_Response_DTO<T> Global_Result<T>(T data, string message, bool Success=false)
        {
            try
            {
                return new Global_Response_DTO<T>
                {
                    Data = data,
                    Message = message,
                    Success = Success
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        //public Global_Response_DTO<IEnumerable<T>> Global_Result_IEnumerable<T>(IEnumerable<T> data, string message, bool Success = false)
        //{
        //    try
        //    {
        //        return new Global_Response_DTO<IEnumerable<T>>
        //        {
        //            Data = data,
        //            Message = message,
        //            Success = Success
        //        };
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
    }
}
