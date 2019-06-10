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
        private readonly IEditShipperCommand _editShippers;
        private readonly IDeleteShipperCommand _deleteShipper;

        public ShippersController(ICreateShipperCommand createShipper, IGetShippersCommand getShippers, IEditShipperCommand editShippers, IDeleteShipperCommand deleteShipper)
        {
            _createShipper = createShipper;
            _getShippers = getShippers;
            _editShippers = editShippers;
            _deleteShipper = deleteShipper;
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
        /// one shipper with given id
        /// </returns>
        /// <response code="200">Successfully returned shipper</response>
        /// <response code="404">Shipper with given id not found</response>
        /// <response code="500">Other server errors</response>
        // GET: api/Shipper/id
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
        /// <response code="404">No shippers found in db based on search criteria</response>
        /// <response code="500">Other server errors</response>
        // GET: api/Shipper
        [HttpGet()]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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

        /// <summary>
        /// Soft-delete a shipper with given id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Shipper soft-deleted from db with given id</response>
        /// <response code="404">No shippers found in db with given id</response>
        /// <response code="500">Other server errors</response>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteShipper.Execute(id);
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
        /// Update shipper information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <response code="200">Updated shipper information</response>
        /// <response code="404">No shippers found in db with given id</response>
        /// <response code="500">Other server errors</response>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Edit(int id, [FromForm] ShipperDto dto)
        {
            dto.Id = id;
            try
            {
                _editShippers.Execute(dto);
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
