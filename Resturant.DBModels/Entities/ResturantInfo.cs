using System;
using System.Collections.Generic;

#nullable disable

namespace Resturant.DBModels.Entities

{
    public partial class ResturantInfo
    {
        public Guid Id { get; set; }
        public Guid ResturantId { get; set; }
        public string Pic { get; set; }

        public virtual Resturant Resturant { get; set; }
    }
}
