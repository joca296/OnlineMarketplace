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
        /// Get shipper by id
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
                var search = new ShipperSearch
                {
                    Id = id
                };
                var result = _getShippers.Execute(search);
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
        /// Get shippers
        /// </summary>
        /// <returns></returns>
        /// <response code="200">List containing all shippers in db with given search criteria</response>
        /// <response code="500">Other server errors</response>
        // GET: api/Shipper
        [HttpGet()]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult GetAll([FromQuery] ShipperSearch search)
        {
            try
            {
                var results = _getShippers.Execute(search);
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
    }
}
