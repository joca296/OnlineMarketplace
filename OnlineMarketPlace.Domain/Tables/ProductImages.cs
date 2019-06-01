using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Domain.Tables
{
    public class ProductImages : BaseTable
    {
        public Products Product { get; set; }
        public Images Image { get; set; }
    }
}
