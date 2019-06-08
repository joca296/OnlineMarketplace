using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineMarketPlace.Application.DataTransfer
{
    public class CreateOrderDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ShipperId { get; set; }

        /// <summary>
        ///     Shipping address must belong to the user with the given ID
        /// </summary>
        [Required]
        public int ShippingAddressId { get; set; }

        /// <summary>
        ///     The number of ProductIds and QuantityPerProduct must match
        /// </summary>
        [Required]
        public List<int> ProductIds { get; set; }

        /// <summary>
        ///     Value must be at least 1
        /// </summary>
        [Required]
        public List<int> QuantityPerProduct { get; set; }

        public List<string> CouponCodes { get; set; }
    }
}
