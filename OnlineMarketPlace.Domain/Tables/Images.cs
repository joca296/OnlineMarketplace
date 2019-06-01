using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Domain.Tables
{
    public class Images : BaseTable
    {
        public string Path { get; set; }
        public string Alt { get; set; }
    }
}
