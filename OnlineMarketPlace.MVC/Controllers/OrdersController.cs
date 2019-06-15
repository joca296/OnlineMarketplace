using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.DataTransfer;
using OnlineMarketPlace.Application.Exceptions;
using OnlineMarketPlace.Application.Searches;

namespace OnlineMarketPlace.MVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IGetShippersCommand _getShippers;
        private readonly ICreateOrderCommand _createOrder;
        private readonly IGetOrdersCommand _getOrders;
        private readonly IDeleteOrdersCommand _deleteOrders;

        public OrdersController(IGetShippersCommand getShippers, ICreateOrderCommand createOrder, IGetOrdersCommand getOrders, IDeleteOrdersCommand deleteOrders)
        {
            _getShippers = getShippers;
            _createOrder = createOrder;
            _getOrders = getOrders;
            _deleteOrders = deleteOrders;
        }

        public IActionResult Create()
        {
            var viewmodel = _getShippers.Execute(new ShipperSearch());
            return View(viewmodel);
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public IActionResult Insert([FromForm] CreateOrderDto dto)
        {
            try
            {
                _createOrder.Execute(dto);
                return Redirect("~/Offers/Create");
            }
            catch (EntityNotFoundException e)
            {
                TempData["message"] = e.Message;
                return Redirect("~/Offers/Create");
            }
            catch (EntityMissmatchException e)
            {
                if (e.Message.Contains("count"))
                {
                    TempData["message"] = e.Message;
                    return Redirect("~/Offers/Create");
                }

                TempData["message"] = e.Message;
                return Redirect("~/Offers/Create");
            }
            catch (ProductNotAvailableException e)
            {
                TempData["message"] = e.Message;
                return Redirect("~/Offers/Create");
            }
            catch (InvalidInputException e)
            {
                TempData["message"] = e.Message;
                return Redirect("~/Offers/Create");
            }
            catch (Exception e)
            {
                TempData["message"] = e.Message;
                return Redirect("~/Offers/Create");
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost()]
        public IActionResult Search([FromForm] OrderSearch search)
        {
            try
            {
                var viewmodel = _getOrders.Execute(search);
                return PartialView(viewmodel);
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

        public IActionResult Delete(int id)
        {
            try
            {
                var search = new OrderSearch
                {
                    Id = id
                };
                _deleteOrders.Execute(search);
                return Redirect("~/Orders");
            }
            catch (EntityNotFoundException e)
            {
                TempData["message"] = e.Message;
                return Redirect("~/Orders");
            }
            catch (Exception e)
            {
                TempData["message"] = e.Message;
                return Redirect("~/Orders");
            }
        }
    }
}