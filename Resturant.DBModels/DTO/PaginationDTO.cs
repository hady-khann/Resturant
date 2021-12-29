using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.DBModels.DTO
{
    public class PaginationDTO
    {
        public int PageNumber { get; set; } = 1;
        public int RowNumber { get; set; } = 10;
        public int Skip => (PageNumber - 1) * RowNumber;
        public int Take => RowNumber;
    }
}
