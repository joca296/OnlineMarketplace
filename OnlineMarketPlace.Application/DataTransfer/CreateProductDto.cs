using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineMarketPlace.Application.DataTransfer
{
    public class CreateProductDto
    {
        /// <summary>
        /// Product name
        /// </summary>
        [Required]
        public string Name { get; set; }


        /// <summary>
        /// Product description
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Price in euros
        /// </summary>
        [Required]
        [Range(0.01, 1000000000)]
        public double UnitPrice { get; set; }

        /// <summary>
        /// Weight in kgs
        /// </summary>
        [Required]
        [Range(0.001, 100000000000)]
        public double UnitWeight { get; set; }

        /// <summary>
        /// Add initial quantity
        /// </summary>
        [Required]
        [Range(1, 100000000)]
        public int QuantityAvailable { get; set; }


        /// <summary>
        /// Category Id must exist in the db
        /// </summary>
        [Required]
        public int CategoryId { get; set; }


        /// <summary>
        /// SubCategory ID must exist in the db and must match the given category ID
        /// </summary>
        [Required]
        public int SubCategoryId { get; set; }

        public List<string> ImagePaths { get; set; }
        public List<string> ImageAlts { get; set; }

        public int? Id { get; set; }
    }
}
