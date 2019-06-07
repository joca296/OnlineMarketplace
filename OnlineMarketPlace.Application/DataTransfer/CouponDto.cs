using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineMarketPlace.Application.DataTransfer
{
    public class CouponDto
    {
        /// <summary>
        /// Coupon name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Coupon code, must be unique
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// Discount in percentage, stacks multiplicatively
        /// </summary>
        [Required]
        [Range(1,100)]
        public double Discount { get; set; }

        /// <summary>
        /// Shipping is automatically 0 if true
        /// </summary>
        [Required]
        public bool FreeShipping { get; set; }
    }
}
