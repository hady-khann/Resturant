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
        public Guid UserId { get; set; }
        public Guid ResturantType { get; set; }
        public string ResturantName { get; set; }
        public string Avatar { get; set; }
        public long Rate { get; set; }
        public long Rated { get; set; }
        public bool Status { get; set; }

        public virtual FoodType ResturantTypeNavigation { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ResturantsFood> ResturantsFoods { get; set; }
        public virtual ICollection<UserOrder> UserOrders { get; set; }
    }
}
