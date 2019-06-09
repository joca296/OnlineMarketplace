using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Domain.Tables
{
    public class Users : BaseTable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
        public ICollection<Orders> Orders { get; set; }
        public ICollection<ShippingAddresses> ShippingAddresses { get; set; }
    }
}
