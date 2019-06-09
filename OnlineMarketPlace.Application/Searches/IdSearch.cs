using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Application.Searches
{
    public class IdSearch
    {
        /// <summary>
        /// Only used when searching for Ids, other filters will be ignored if this has a value
        /// </summary>
        public int? Id { get; set; }
    }
}
