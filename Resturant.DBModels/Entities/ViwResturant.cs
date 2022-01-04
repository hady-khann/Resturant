using System;
using System.Collections.Generic;

#nullable disable

namespace Resturant.DBModels.Entities
{
    public partial class ViwResturant
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid FoodId { get; set; }
        public string ResturantName { get; set; }
        public string Type { get; set; }
        public string Avatar { get; set; }
        public double Rate { get; set; }
        public long Rated { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
    }
}
