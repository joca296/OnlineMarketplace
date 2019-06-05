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

        public UsersController(ICreateUserCommand createUser, IActivateUserCommand activateUser)
        {
            _createUser = createUser;
            _activateUser = activateUser;
        }

        // POST: api/Users
        [HttpPost]
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
            catch (EntityAlreadyExists e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

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
    }
}
