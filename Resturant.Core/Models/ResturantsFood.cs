using System;
using System.Collections.Generic;

#nullable disable

namespace Resturant.Core.Models
{
    public partial class ResturantsFood
    {
        public Guid Id { get; set; }
        public Guid ResturantId { get; set; }
        public Guid FoodId { get; set; }

        public virtual Food Resturant { get; set; }
        public virtual Resturant ResturantNavigation { get; set; }
    }
}
