using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Application.DataTransfer
{
    class CreateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public double UnitWeight { get; set; }
        public int QuantityAvailable { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
       
        //add dto properties later
    }
}
