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
    public class UsersController : ControllerBase
    {
        private readonly ICreateUserCommand _createUser;
        private readonly IActivateUserCommand _activateUser;
        private readonly ICreateShippingAddressCommand _createShippingAddress;

        public UsersController(ICreateUserCommand createUser, IActivateUserCommand activateUser, ICreateShippingAddressCommand createShippingAddress)
        {
            _createUser = createUser;
            _activateUser = activateUser;
            _createShippingAddress = createShippingAddress;
        }

        /// <summary>
        /// Register a user and send an email in order to activate the account
        /// </summary>
        /// <returns></returns>
        /// <response code="200">User has successfully registered and an email has been sent in order to activate it</response>
        /// <response code="422">The given role ID doesn't exist in the database</response>
        /// <response code="409">A user with the given email already exists</response>
        /// <response code="500">Other server errors</response>
        // POST: api/Users
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromForm] CreateUserDto dto)
        {
            try
            {
                _createUser.Execute(dto);
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return UnprocessableEntity(e.Message);
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
        /// Activate user account with a key generated in email sent during registration
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        // GET: api/Users/Activate/key
        [HttpGet("Activate/{key}")]
        public IActionResult ActivateUser(string key)
        {
            try
            {
                _activateUser.Execute(key);
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        /// <summary>
        /// Add a shipping address to a user, assuming the user entered valid locations
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <response code="200">Shipping address has been successfully added to database</response>
        /// <response code="422">The given user ID doesn't exist in the database</response>
        /// <response code="409">A shipping address already exists in the database that belongs to the user with given ID</response>
        /// <response code="500">Other server errors</response>
        // POST: api/Users/ShippingAddresses
        [HttpPost("ShippingAddresses")]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
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
                return UnprocessableEntity(e.Message);
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
    }
}
