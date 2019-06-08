using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.DataTransfer;
using OnlineMarketPlace.Application.Exceptions;
using OnlineMarketPlace.Application.Helpers;

namespace OnlineMarketPlace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ICreateProductCommand _createProduct;

        public ProductsController(ICreateProductCommand createProduct)
        {
            _createProduct = createProduct;
        }

        /// <summary>
        /// Create a product
        /// </summary>
        /// <response code="200">Product successfully created</response>
        /// <response code="400">Image not uploaded, file not an image or image too large</response>
        /// <response code="404">Category or subcategory with given ID doesn't exist in db</response>
        /// <response code="409">Category and subcategory with given IDs don't match to eachother in db</response>
        /// <response code="500">Other server issues</response>
        // POST: api/Products
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromForm] Product p)
        {
            try
            {
                if (p.Images.Count == 0)
                    return BadRequest("Image not uploaded");

                List<string> productPaths = new List<string>();
                List<string> productAlts = new List<string>();

                foreach(var image in p.Images)
                {
                    var extension = Path.GetExtension(image.FileName);

                    if (!ImageUploadHelper.AllowedExtensions.Contains(extension))
                        return BadRequest("One of the files is not an image");

                    if (image.Length > ImageUploadHelper.MaxSize)
                        return BadRequest("One of the images is too large for uploading. Max = 8MB");

                    var newFileName = DateTime.Now.ToBinary().ToString() + "_" + image.FileName;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", newFileName);
                    image.CopyTo(new FileStream(filePath, FileMode.Create));

                    productPaths.Add(filePath.ToString());
                    productAlts.Add(newFileName);
                }

                var dto = new ProductDto
                {
                    Name = p.Name,
                    Description = p.Description,
                    UnitPrice = p.UnitPrice,
                    UnitWeight = p.UnitWeight,
                    QuantityAvailable = p.QuantityAvailable,
                    CategoryId = p.CategoryId,
                    SubCategoryId = p.SubCategoryId,
                    ImagePaths = productPaths,
                    ImageAlts = productAlts
                };

                _createProduct.Execute(dto);
                return Ok();

            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (EntityMissmatchException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }

    public class Product
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


        /// <summary>
        /// Accepts multiple images, current swagger UI spec doesn't support multi file upload, use postman or similar for testing
        /// </summary>
        [Required]
        public IFormFileCollection Images { get; set; }
    }
}

