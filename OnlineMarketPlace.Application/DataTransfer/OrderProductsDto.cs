using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Application.DataTransfer
{
    public class OrderProductsDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string CategoryName { get; set; }

        public string SubCategoryName { get; set; }

        public double UnitWeight { get; set; }

        public double UnitPrice { get; set; }

        public int QuantityOrdered { get; set; }
    }
}
