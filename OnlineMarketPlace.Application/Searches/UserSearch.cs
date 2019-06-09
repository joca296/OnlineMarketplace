using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Application.Searches
{
    public class UserSearch : IdSearch
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
