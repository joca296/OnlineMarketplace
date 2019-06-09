using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Domain.Tables
{
    public class Categories : BaseTable
    {
        public string Name { get; set; }
        public ICollection<SubCategories> SubCategories { get; set; }
    }
}
