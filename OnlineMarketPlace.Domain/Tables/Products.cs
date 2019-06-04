using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Domain.Tables
{
    public class Products : BaseTable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public double UnitWeight { get; set; }
        public int QuantityAvailable { get; set; }
        public Categories Category { get; set; }
        public SubCategories SubCategory { get; set; }
    }
}
