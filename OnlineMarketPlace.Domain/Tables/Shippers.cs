using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Domain.Tables
{
    public class Shippers : BaseTable
    {
        public string Name { get; set; }
        public double FreightPerKilo { get; set; }
        public double? FreightBase { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }
}
