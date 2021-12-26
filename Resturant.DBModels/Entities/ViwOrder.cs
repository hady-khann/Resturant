using System;
using System.Collections.Generic;

#nullable disable

namespace Resturant.DBModels.Entities
{
    public partial class ViwOrder
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public Guid FoodId { get; set; }
        public Guid ResturantId { get; set; }
        public string ResturantName { get; set; }
        public Guid ResturantType { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public int FoodCount { get; set; }
        public string FoodName { get; set; }
        public string Avatar { get; set; }
        public string Pic { get; set; }
        public double Rate { get; set; }
        public string Description { get; set; }
    }
}
