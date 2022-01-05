using System;
using System.Collections.Generic;

#nullable disable

namespace Resturant.DBModels.DTO
{
    public partial class ResturantDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ResturantType { get; set; }
        public string ResturantName { get; set; }
        public string Avatar { get; set; }
        public long Rate { get; set; }
        public long Rated { get; set; }
        public bool Status { get; set; }
    }
}
