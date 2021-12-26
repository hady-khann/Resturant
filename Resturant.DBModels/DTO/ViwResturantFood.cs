﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Resturant.DBModels.DTO
{
    public partial class ViwResturantFoodDTO
    {
        public Guid IdFood { get; set; }
        public Guid TypeId { get; set; }
        public string FoodName { get; set; }
        public string PicFood { get; set; }
        public string Description { get; set; }
        public Guid IdType { get; set; }
        public string Type { get; set; }
        public Guid IdResturant { get; set; }
        public Guid ResturantType { get; set; }
        public string ResturantName { get; set; }
        public string PicResturant { get; set; }
        public string Address { get; set; }
        public double Rate { get; set; }
        public Guid IdResFood { get; set; }
        public Guid ResturantId { get; set; }
        public Guid FoodId { get; set; }
    }
}