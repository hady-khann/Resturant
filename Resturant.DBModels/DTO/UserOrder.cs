using System;
using System.Collections.Generic;

#nullable disable

namespace Resturant.DBModels.DTO
{
    public partial class UserOrderDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ResturantId { get; set; }
        public Guid FoodId { get; set; }
        public Guid Code { get; set; }
        public int FoodCount { get; set; }
    }
}
