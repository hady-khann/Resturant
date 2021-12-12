using System;
using System.Collections.Generic;

#nullable disable

namespace Resturant.DBModels.Entities
{
    public partial class UserOrder
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ResturantId { get; set; }
        public Guid FoodId { get; set; }
        public int FoodCount { get; set; }

        public virtual Food Food { get; set; }
        public virtual Resturant Resturant { get; set; }
        public virtual User User { get; set; }
    }
}
