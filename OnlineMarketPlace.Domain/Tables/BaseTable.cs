using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Domain.Tables
{
    public class BaseTable
    {
        public int id { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime? dateUpdated { get; set; }
        public bool active { get; set; }
        public string key { get; set; }
    }
}
