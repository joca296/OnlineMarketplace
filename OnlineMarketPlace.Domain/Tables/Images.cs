using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Domain.Tables
{
    public class Images : BaseTable
    {
        public string path { get; set; }
        public string alt { get; set; }
    }
}
