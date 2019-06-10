using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineMarketPlace.Application.DataTransfer
{
    public class DeleteShippingAddressDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ShippingAddressId { get; set; }
    }
}
