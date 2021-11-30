using System;
using System.Collections.Generic;

#nullable disable

namespace Resturant.DBModels.Entities

{
    public partial class Resturant
    {
        public Resturant()
        {
            ResturantsFoods = new HashSet<ResturantsFood>();
            UserOrders = new HashSet<UserOrder>();
        }

        public Guid Id { get; set; }
        public Guid ResturantType { get; set; }
        public string ResturantName { get; set; }
        public string Pic { get; set; }
        public string Address { get; set; }
        public double Rate { get; set; }

        public virtual FoodType ResturantTypeNavigation { get; set; }
        public virtual ICollection<ResturantsFood> ResturantsFoods { get; set; }
        public virtual ICollection<UserOrder> UserOrders { get; set; }
    }
}
