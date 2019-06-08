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
    public class RolesController : ControllerBase
    {
        private readonly ICreateRoleCommand _createRole;

        public RolesController(ICreateRoleCommand createRole)
        {
            _createRole = createRole;
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
    }
}
