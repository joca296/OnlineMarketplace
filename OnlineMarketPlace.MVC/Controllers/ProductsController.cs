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
using OnlineMarketPlace.Application.Searches;

namespace OnlineMarketPlace.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ICreateProductCommand _createProduct;
        private readonly IGetProductsCommand _getProducts;
        private readonly IDeleteProductsCommand _deleteProducts;
        private readonly IEditProductCommand _editProduct;

        public ProductsController(ICreateProductCommand createProduct, IGetProductsCommand getProducts, IDeleteProductsCommand deleteProducts, IEditProductCommand editProduct)
        {
            _createProduct = createProduct;
            _getProducts = getProducts;
            _deleteProducts = deleteProducts;
            _editProduct = editProduct;
        }

        public IActionResult Index()
        {
            var viewmodel = _getProducts.Execute(new ProductSearch());
            return View(viewmodel);
        }

        public IActionResult Get(int id)
        {
            try
            {
                var search = new ProductSearch
                {
                    Id = id
                };
                var viewmodel = _getProducts.Execute(search);
                return View(viewmodel.First());
            }
            catch (EntityNotFoundException e)
            {
                TempData["message"] = e.Message;
                return Redirect("~/");
            }
            catch (Exception e)
            {
                TempData["message"] = e.Message;
                return Redirect("~/");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public IActionResult Insert([FromForm] Product p)
        {
            try
            {
                if (p.Images.Count == 0)
                    return BadRequest("Image not uploaded");

                List<string> productPaths = new List<string>();
                List<string> productAlts = new List<string>();

                foreach (var image in p.Images)
                {
                    var extension = Path.GetExtension(image.FileName);

                    if (!ImageUploadHelper.AllowedExtensions.Contains(extension))
                        return BadRequest("One of the files is not an image");

                    if (image.Length > ImageUploadHelper.MaxSize)
                        return BadRequest("One of the images is too large for uploading. Max = 8MB");

                    var newFileName = DateTime.Now.ToBinary().ToString() + "_" + image.FileName;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", newFileName);
                    image.CopyTo(new FileStream(filePath, FileMode.Create));

                    productPaths.Add(@"uploads\" + newFileName);
                    productAlts.Add(newFileName);
                }

                var dto = new CreateProductDto
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
                return Redirect("~/");

            }
            catch (EntityNotFoundException e)
            {
                TempData["message"] = e.Message;
                return Redirect("~/Products/Create");
            }
            catch (EntityMissmatchException e)
            {
                TempData["message"] = e.Message;
                return Redirect("~/Products/Create");
            }
            catch (Exception e)
            {
                TempData["message"] = e.Message;
                return Redirect("~/Products/Create");
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var search = new ProductSearch
                {
                    Id = id
                };
                _deleteProducts.Execute(search);
                return Redirect("~/");
            }
            catch (EntityNotFoundException e)
            {
                TempData["message"] = e.Message;
                return Redirect("~/");
            }
            catch (Exception e)
            {
                TempData["message"] = e.Message;
                return Redirect("~/");
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                var search = new ProductSearch
                {
                    Id = id
                };
                var viewmodel = _getProducts.Execute(search);
                return View(viewmodel.First());
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, [FromForm] ProductEdit p)
        {
            try
            {
                List<string> productPaths = new List<string>();
                List<string> productAlts = new List<string>();

                if(p.Images != null && p.Images.Count != 0)
                    foreach (var image in p.Images)
                    {
                        var extension = Path.GetExtension(image.FileName);

                        if (!ImageUploadHelper.AllowedExtensions.Contains(extension))
                        {
                            TempData["message"] = "One of the files is not an image";
                            return Redirect($"~/Products/Edit/{id}");
                        }

                        if (image.Length > ImageUploadHelper.MaxSize)
                        {
                            TempData["message"] = "One of the images is too large for uploading. Max = 8MB";
                            return Redirect($"~/Products/Edit/{id}");
                        }

                        var newFileName = DateTime.Now.ToBinary().ToString() + "_" + image.FileName;
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", newFileName);
                        image.CopyTo(new FileStream(filePath, FileMode.Create));

                        productPaths.Add(@"uploads\" + newFileName);
                        productAlts.Add(newFileName);
                    }

                var dto = new CreateProductDto
                {
                    Name = p.Name,
                    Description = p.Description,
                    UnitPrice = p.UnitPrice,
                    UnitWeight = p.UnitWeight,
                    QuantityAvailable = p.QuantityAvailable,
                    CategoryId = p.CategoryId,
                    SubCategoryId = p.SubCategoryId,
                    ImagePaths = productPaths,
                    ImageAlts = productAlts,
                    Id = id
                };

                _editProduct.Execute(dto);
                return Redirect("~/");
            }
            catch (EntityNotFoundException e)
            {
                TempData["message"] = e.Message;
                return Redirect($"~/Products/Edit/{id}");
            }
            catch (EntityMissmatchException e)
            {
                TempData["message"] = e.Message;
                return Redirect($"~/Products/Edit/{id}");
            }
            catch (Exception e)
            {
                TempData["message"] = e.Message;
                return Redirect($"~/Products/Edit/{id}");
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

    public class ProductEdit
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
        /// Optional, Accepts multiple images, current swagger UI spec doesn't support multi file upload, use postman or similar for testing
        /// </summary>
        public IFormFileCollection Images { get; set; }
    }
}