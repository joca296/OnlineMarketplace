using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Application.Searches
{
    public class ShipperSearch : NameSearch
    {
        public double? MinFreightPerKilo { get; set; }
        public double? MaxFreightPerKilo { get; set; }
        public double? MinFreightBase { get; set; }
        public double? MaxFreightBase { get; set; }
    }
}
