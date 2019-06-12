using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.Exceptions;
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
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}