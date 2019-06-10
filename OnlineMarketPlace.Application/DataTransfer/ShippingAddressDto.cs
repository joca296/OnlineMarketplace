using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineMarketPlace.Application.DataTransfer
{
    public class ShippingAddressDto
    {
        public int? ShippingAddressId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Country { get; set; }
        public string State { get; set; }
        [Required]        
        public string City { get; set; }
        [Required]        
        public string Address { get; set; }
        [Required]
        public string PostalCode { get; set; }
    }
}
