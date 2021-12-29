using System;
using System.Collections.Generic;

#nullable disable

namespace Resturant.DBModels.Entities
{
    public partial class ViwFood
    {
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public string FoodName { get; set; }
        public string Pic { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
