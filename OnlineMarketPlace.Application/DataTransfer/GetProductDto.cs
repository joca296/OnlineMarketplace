using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Application.DataTransfer
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public double UnitWeight { get; set; }
        public int QuantityAvailable { get; set; }
        public CategoryDto Category { get; set; }
        public SubCategoryDto SubCategory { get; set; }
        public ICollection<string> ImagePaths { get; set; }
        public ICollection<string> ImageAlts { get; set; }
    }
}
