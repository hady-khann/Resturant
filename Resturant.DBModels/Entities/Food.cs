using System;
using System.Collections.Generic;

#nullable disable

namespace Resturant.DBModels.Entities
{
    public partial class Food
    {
        public Food()
        {
            ResturantsFoods = new HashSet<ResturantsFood>();
            UserOrders = new HashSet<UserOrder>();
        }

        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public string FoodName { get; set; }
        public string Pic { get; set; }
        public string Description { get; set; }

        public virtual FoodType Type { get; set; }
        public virtual ICollection<ResturantsFood> ResturantsFoods { get; set; }
        public virtual ICollection<UserOrder> UserOrders { get; set; }
    }
}
