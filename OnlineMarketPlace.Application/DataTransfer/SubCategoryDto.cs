using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineMarketPlace.Application.DataTransfer
{
    public class SubCategoryDto
    {
        /// <summary>
        /// Must be a unique name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Must be an id that exists in the category table
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        public int? Id { get; set; }
    }
}
