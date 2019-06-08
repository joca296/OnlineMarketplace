using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineMarketPlace.Application.DataTransfer
{
    public class ShipperDto
    {
        /// <summary>
        /// Must be a unique name
        /// </summary>
        [Required]
        public string Name { get; set; }

        [Required]
        public double FreightPerKilo { get; set; }

        public double? FreightBase { get; set; }

        public int? Id { get; set; }
    }
}
