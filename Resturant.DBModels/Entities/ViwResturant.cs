using System;
using System.Collections.Generic;

#nullable disable

namespace Resturant.DBModels.Entities
{
    public partial class ViwResturant
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
