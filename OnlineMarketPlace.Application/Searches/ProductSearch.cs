using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Application.Searches
{
    public class ProductSearch : IdSearch
    {
        public string Name { get; set; }
        public double? MinUnitPrice { get; set; }
        public double? MaxUnitPrice { get; set; }
        public int? QuantityAvailable { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
    }
}
