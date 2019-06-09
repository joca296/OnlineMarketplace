using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Application.Searches
{
    public class OrderSearch : IdSearch
    {
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double? MinTotalPrice { get; set; }
        public double? MaxTotalPrice { get; set; }
        public int? ShipperId { get; set; }
        public string ShipperName { get; set; }
        public bool? Shipped { get; set; }
        public bool? Delivered { get; set; }
        public DateTime? MinDateOrdered { get; set; }
        public DateTime? MaxDateOrdered { get; set; }
        public DateTime? MinDateShipped { get; set; }
        public DateTime? MaxDateShipped { get; set; }
        public DateTime? MinDateDelivered { get; set; }
        public DateTime? MaxDateDelivered { get; set; }
    }
}
