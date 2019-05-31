using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Domain.Tables
{
    public class Users : BaseTable
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string eMail { get; set; }
        public string password { get; set; }
        public Roles role { get; set; }
        public ICollection<ShippingAddresses> shippingAddresses { get; set; }
    }
}
