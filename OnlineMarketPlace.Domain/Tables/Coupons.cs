using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Domain.Tables
{
    public class Coupons : BaseTable
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public double Discount { get; set; }
        public bool FreeShipping { get; set; }
    }
}
