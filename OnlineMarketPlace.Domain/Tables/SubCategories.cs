using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Domain.Tables
{
    public class SubCategories : BaseTable
    {
        public string Name { get; set; }
        public Categories Category { get; set; }
    }
}
