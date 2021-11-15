using System;
using System.Collections.Generic;

#nullable disable

namespace Resturant.Core.Models
{
    public partial class FoodType
    {
        public FoodType()
        {
            Foods = new HashSet<Food>();
            Resturants = new HashSet<Resturant>();
        }

        public Guid Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Food> Foods { get; set; }
        public virtual ICollection<Resturant> Resturants { get; set; }
    }
}
