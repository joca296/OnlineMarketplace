using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Domain.Tables
{
    public class Orders : BaseTable
    {
        public double TotalPrice { get; set; }
        public double TotalFreight { get; set; }
        public Users User { get; set; }
        public Shippers Shipper { get; set; }
        public ShippingAddresses ShippingAddress { get; set; }
        public DateTime? DateShipped { get; set; }
        public DateTime? DateDelivered { get; set; }
        public ICollection<OrderProducts> OrderProducts { get; set; }
        public ICollection<OrderCoupons> OrderCoupons { get; set; }
    }
}
