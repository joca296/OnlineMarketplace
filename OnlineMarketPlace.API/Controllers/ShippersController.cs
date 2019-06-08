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
        private readonly IGetShippersCommand _getShippers;

        public ShippersController(ICreateShipperCommand createShipper, IGetShippersCommand getShippers)
        {
            _createShipper = createShipper;
            _getShippers = getShippers;
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

        /// <summary>
        /// Get shipper
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// null if given id is invalid OR
        /// one shipper with given id
        /// </returns>
        /// <response code="200">Successfully returned shipper</response>
        /// <response code="404">Shipper with given id not found</response>
        /// <response code="500">Other server errors</response>
        // GET: api/Shipper/id
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult Get(int id)
        {
            try
            {
                var results = _getShippers.Execute(id);
                return Ok(results.First());
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
        /// Get shippers
        /// </summary>
        /// <returns>
        /// List containing all shippers in db if id is NULL, 
        /// </returns>
        /// <response code="500">Other server errors</response>
        // GET: api/Shipper
        [HttpGet()]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult GetAll()
        {
            try
            {
                var results = _getShippers.Execute(null);
                return Ok(results);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
