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
    public class RolesController : ControllerBase
    {
        private readonly ICreateRoleCommand _createRole;
        private readonly IGetRolesCommand _getRoles;

        public RolesController(ICreateRoleCommand createRole, IGetRolesCommand getRoles)
        {
            _createRole = createRole;
            _getRoles = getRoles;
        }

        /// <summary>
        /// Creates a new role
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <response code="200">Role has been successfully added to the database</response>
        /// <response code="409">Role with the given name already exists in the database</response>
        /// <response code="500">Other server errors</response>
        // POST: api/Roles
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromForm] RoleDto dto)
        {
            try
            {
                _createRole.Execute(dto);
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
        /// Get role by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// null if given id is invalid OR
        /// one role with given id
        /// </returns>
        /// <response code="200">Successfully returned role</response>
        /// <response code="404">Role with given id not found</response>
        /// <response code="500">Other server errors</response>
        // GET: api/Role/id
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult Get(int id)
        {
            try
            {
                var search = new NameSearch
                {
                    Id = id
                };
                var result = _getRoles.Execute(search);
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
        /// Get roles
        /// </summary>
        /// <returns></returns>
        /// <response code="200">List containing all roles in db with given search criteria</response>
        /// <response code="500">Other server errors</response>
        // GET: api/Role
        [HttpGet()]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult GetAll([FromQuery] NameSearch search)
        {
            try
            {
                var results = _getRoles.Execute(search);
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
