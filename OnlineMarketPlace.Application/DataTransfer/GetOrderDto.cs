using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Application.DataTransfer
{
    public class GetOrderDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserEmail { get; set; }

        public double TotalPrice { get; set; }

        public double TotalFreight { get; set; }

        public ShipperDto Shipper { get; set; }

        public ShippingAddressDto ShippingAddress { get; set; }

        public IEnumerable<OrderProductsDto> Products { get; set; }

        public DateTime DateOrdered { get; set; }

        public DateTime? DateShipped { get; set; }

        public DateTime? DateDelivered { get; set; }
    }
}
