using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Domain.Tables
{
    public class OrderProducts : BaseTable
    {
        public Orders Order { get; set; }
        public Products Product { get; set; }
        public int Quantity { get; set; }
        public double TotalWeight { get; set; }
        public double TotalPrice { get; set; }
    }
}
