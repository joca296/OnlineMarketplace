using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.DataTransfer;
using OnlineMarketPlace.Application.Exceptions;

namespace OnlineMarketPlace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICreateOrderCommand _coreateOrder;

        public OrdersController(ICreateOrderCommand coreateOrder)
        {
            _coreateOrder = coreateOrder;
        }

        /// <summary>
        ///     Creates an order
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <response code="200">Order has been successfully entered into the database</response>
        /// <response code="404">The given user, shipping address, product or coupon code doesn't exist in the database</response>
        /// <response code="400">Product count and QuantityPerProduct array don't match OR given product doesn't have enough units available for the order OR invalid quantity input</response>
        /// <response code="422">The shipping address doesn't belong to the given user</response>
        /// <response code="500">Other server issues</response>
        // POST: api/Orders
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromForm] CreateOrderDto dto)
        {
            try
            {
                _coreateOrder.Execute(dto);
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (EntityMissmatchException e)
            {
                if (e.Message.Contains("count"))
                    return BadRequest(e.Message);

                return UnprocessableEntity(e.Message);
            }
            catch (ProductNotAvailableException e)
            {
                return BadRequest(e.Message);
            }
            catch (InvalidInputException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
