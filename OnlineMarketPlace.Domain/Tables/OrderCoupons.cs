using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Domain.Tables
{
    public class OrderCoupons : BaseTable
    {
        public Orders Order { get; set; }
        public Coupons Coupon { get; set; }
    }
}
