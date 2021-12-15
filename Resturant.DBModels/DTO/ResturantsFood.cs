using System;
using System.Collections.Generic;

#nullable disable

namespace Resturant.DBModels.DTO
{
    public partial class ResturantsFoodDTO
    {
        public Guid Id { get; set; }
        public Guid ResturantId { get; set; }
        public Guid FoodId { get; set; }
    }
}
