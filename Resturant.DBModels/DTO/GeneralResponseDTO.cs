using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.DBModels.DTO
{
        public class Global_Response_DTO<T>
        {
            public T Data { get; set; }
            public string Message { get; set; }
            public bool Success { get; set; }
        }
}
