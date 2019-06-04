using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.DataTransfer;

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

        // POST: api/Roles
        [HttpPost]
        public IActionResult Post([FromForm] CreateRoleDto dto)
        {
            try
            {
                _createRole.Execute(dto);
                return Ok();
            }
            catch (Exception e)
            {
                return UnprocessableEntity(e.Message);
            }
        }
    }
}
