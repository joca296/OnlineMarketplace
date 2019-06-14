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
    public class OffersController : Controller
    {
        private readonly IGetShippersCommand _getShippers;
        private readonly ICreateOrderCommand _createOrder;

        public OffersController(IGetShippersCommand getShippers, ICreateOrderCommand createOrder)
        {
            _getShippers = getShippers;
            _createOrder = createOrder;
        }

        public IActionResult Create()
        {
            var viewmodel = _getShippers.Execute(new ShipperSearch());
            return View(viewmodel);
        }

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
    }
}