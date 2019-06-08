using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineMarketPlace.Application.DataTransfer
{
    public class CategoryDto
    {
        /// <summary>
        /// Must be a unique name
        /// </summary>
        [Required]
        public string Name { get; set; }

        public int? Id { get; set; }
    }
}
