using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.DataTransfer;
using OnlineMarketPlace.Application.Exceptions;
using OnlineMarketPlace.Application.Searches;

namespace OnlineMarketPlace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICreateOrderCommand _createOrder;
        private readonly IGetOrdersCommand _getOrders;
        private readonly IDeleteOrdersCommand _deleteOrders;

        public OrdersController(ICreateOrderCommand createOrder, IGetOrdersCommand getOrders, IDeleteOrdersCommand deleteOrders)
        {
            _createOrder = createOrder;
            _getOrders = getOrders;
            _deleteOrders = deleteOrders;
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
                _createOrder.Execute(dto);
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

        /// <summary>
        /// Get order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// one user with given id
        /// </returns>
        /// <response code="200">Successfully returned order</response>
        /// <response code="404">Order with given id not found</response>
        /// <response code="500">Other server errors</response>
        // GET: api/Role/id
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Get(int id)
        {
            try
            {
                var search = new OrderSearch
                {
                    Id = id
                };
                var result = _getOrders.Execute(search);
                return Ok(result.First());
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

        /// <summary>
        /// Get orders
        /// </summary>
        /// <returns></returns>
        /// <response code="200">List containing all orders in db with given search criteria</response>
        /// <response code="404">No orders found in db based on search criteria</response>
        /// <response code="500">Other server errors</response>
        // GET: api/Role
        [HttpGet()]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetAll([FromForm] OrderSearch search)
        {
            try
            {
                var results = _getOrders.Execute(search);
                return Ok(results);
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

        /// <summary>
        /// Delete order by id, sends an email to the recipient if the order is not shipped
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// </returns>
        /// <response code="200">Successfully soft-deleted order</response>
        /// <response code="404">Order with given id not found</response>
        /// <response code="500">Other server errors</response>
        // DELETE: api/Products/id
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete(int id)
        {
            try
            {
                var search = new OrderSearch
                {
                    Id = id
                };
                _deleteOrders.Execute(search);
                return Ok();
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

        /// <summary>
        /// Delete orders, sends an email to the recipient if the order is not shipped
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Deleted all orders in db with given search criteria</response>
        /// <response code="404">No orders found in db based on search criteria</response>
        /// <response code="500">Other server errors</response>
        // DELETE: api/Products
        [HttpDelete()]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteAll([FromQuery] OrderSearch search)
        {
            try
            {
                _deleteOrders.Execute(search);
                return Ok();
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
