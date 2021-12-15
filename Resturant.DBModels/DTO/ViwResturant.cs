using System;
using System.Collections.Generic;

#nullable disable

namespace Resturant.DBModels.DTO
{
    public partial class ViwResturantDTO
    {
        public Guid Id { get; set; }
        public Guid ResturantType { get; set; }
        public string ResturantName { get; set; }
        public string Pic { get; set; }
        public string Address { get; set; }
        public double Rate { get; set; }
        public string Type { get; set; }
    }
}
