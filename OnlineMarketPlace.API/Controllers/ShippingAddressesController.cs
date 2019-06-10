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
    public class ShippingAddressesController : ControllerBase
    {
        private readonly ICreateShippingAddressCommand _createShippingAddress;
        private readonly IGetShippingAddressesCommand _getShippingAddresses;
        private readonly IEditShippingAddressCommand _editShippingAddress;
        private readonly IDeleteShippingAddressCommand _deleteShippingAddress;

        public ShippingAddressesController(ICreateShippingAddressCommand createShippingAddress, IGetShippingAddressesCommand getShippingAddresses, IEditShippingAddressCommand editShippingAddress, IDeleteShippingAddressCommand deleteShippingAddress)
        {
            _createShippingAddress = createShippingAddress;
            _getShippingAddresses = getShippingAddresses;
            _editShippingAddress = editShippingAddress;
            _deleteShippingAddress = deleteShippingAddress;
        }

        /// <summary>
        /// Add a shipping address to a user, assuming the user entered valid locations
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <response code="200">Shipping address has been successfully added to database</response>
        /// <response code="404">The given user ID doesn't exist in the database</response>
        /// <response code="409">A shipping address already exists in the database that belongs to the user with given ID</response>
        /// <response code="500">Other server errors</response>
        [HttpPost()]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult AddShippingAddress([FromForm] ShippingAddressDto dto)
        {
            try
            {
                _createShippingAddress.Execute(dto);
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (EntityAlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        /// <summary>
        /// Get shipping addresses with given user ID
        /// </summary>
        /// <response code="200">Returns a list of shipping addresses that belong to the user</response>
        /// <response code="404">The given user ID doesn't exist in the database</response>
        /// <response code="500">Other server errors</response>
        [HttpGet("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Get(int userId)
        {
            try
            {
                var results = _getShippingAddresses.Execute(userId);
                return Ok(results);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        /// <summary>
        /// Edit shipping address, cancels all unshipped orders if there were any orders going to that address, recipient is notified
        /// </summary>
        /// <response code="200">Successfully edited shipping address</response>
        /// <response code="404">Shipping address or user not found</response>
        /// <response code="409">Shipping address aready exists that belongs to the given user id</response>
        /// <response code="422">Shipping addres and user don't match</response>
        /// <response code="500">Other server errors</response>
        [HttpPut("{addressId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult Put(int addressId, [FromForm] ShippingAddressDto dto)
        {
            dto.ShippingAddressId = addressId;
            try
            {
                _editShippingAddress.Execute(dto);
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (EntityAlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
            catch (EntityMissmatchException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        /// <summary>
        /// Soft-deletes a shipping address, cancels all unshipped orders if there were any orders going to that address, recipient is notified
        /// </summary>
        /// <response code="200">Successfully soft-deleted shipping address</response>
        /// <response code="404">Shipping address or user not found</response>
        /// <response code="422">Shipping addres and user don't match</response>
        /// <response code="500">Other server errors</response>
        [HttpDelete("{addressId}/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult Delete(int addressId, int userId)
        {
            var dto = new DeleteShippingAddressDto
            {
                ShippingAddressId = addressId,
                UserId = userId
            };
            try
            {
                _deleteShippingAddress.Execute(dto);
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (EntityMissmatchException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}