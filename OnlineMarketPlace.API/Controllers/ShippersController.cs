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
    public class ShippersController : ControllerBase
    {
        private readonly ICreateShipperCommand _createShipper;

        public ShippersController(ICreateShipperCommand createShipper)
        {
            _createShipper = createShipper;
        }

        /// <summary>
        /// Creates a new shipper
        /// </summary>
        /// <param name="dto"></param>
        /// <response code="200">Shipper has been successfully created</response>
        /// <response code="409">Shipper with given name already exists</response>
        /// <response code="500">Other server errors</response>
        // POST: api/Shippers
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromForm] ShipperDto dto)
        {
            try
            {
                _createShipper.Execute(dto);
                return Ok();
            }
            catch (EntityAlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
