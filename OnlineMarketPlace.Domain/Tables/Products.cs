using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Domain.Tables
{
    public class Products : BaseTable
    {
        public string name { get; set; }
        public string description { get; set; }
        public double unitPrice { get; set; }
        public int quantityAvailable { get; set; }
        public ICollection<Images> images { get; set; }
        public Categories category { get; set; }
    }
}
