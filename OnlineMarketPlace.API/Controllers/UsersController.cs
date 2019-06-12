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
    public class UsersController : ControllerBase
    {
        private readonly ICreateUserCommand _createUser;
        private readonly IActivateUserCommand _activateUser;
        private readonly IGetUsersCommand _getUsers;
        private readonly IDeleteUserCommand _deleteUser;
        private readonly IEditUserEmailCommand _editUserEmail;
        private readonly IEditUserPasswordCommand _editUserPassword;
        private readonly IAuthenticateUserCommand _authUser;

        public UsersController(ICreateUserCommand createUser, IActivateUserCommand activateUser, IGetUsersCommand getUsers, IDeleteUserCommand deleteUser, IEditUserEmailCommand editUserEmail, IEditUserPasswordCommand editUserPassword, IAuthenticateUserCommand authUser)
        {
            _createUser = createUser;
            _activateUser = activateUser;
            _getUsers = getUsers;
            _deleteUser = deleteUser;
            _editUserEmail = editUserEmail;
            _editUserPassword = editUserPassword;
            _authUser = authUser;
        }

        /// <summary>
        /// Register a user and send an email in order to activate the account
        /// </summary>
        /// <returns></returns>
        /// <response code="200">User has successfully registered and an email has been sent in order to activate it</response>
        /// <response code="404">The given role ID doesn't exist in the database</response>
        /// <response code="409">A user with the given email already exists</response>
        /// <response code="500">Other server errors</response>
        // POST: api/Users
        [HttpPost()]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// one user with given id
        /// </returns>
        /// <response code="200">Successfully returned user</response>
        /// <response code="404">User with given id not found</response>
        /// <response code="500">Other server errors</response>
        // GET: api/Role/id
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Get(int id)
        {
            try
            {
                var search = new UserSearch
                {
                    Id = id
                };
                var result = _getUsers.Execute(search);
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
        /// Get users
        /// </summary>
        /// <returns></returns>
        /// <response code="200">List containing all roles in db with given search criteria</response>
        /// <response code="404">No roles found in db based on search criteria</response>
        /// <response code="500">Other server errors</response>
        // GET: api/Role
        [HttpGet()]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetAll([FromQuery] UserSearch search)
        {
            try
            {
                var results = _getUsers.Execute(search);
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
        /// Soft-deletes a user with given id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">User soft-deleted and his/her shipping addresses and orders</response>
        /// <response code="404">No users found in db based on search criteria</response>
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
                _deleteUser.Execute(id);
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
        /// Edit user password
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <response code="200">User successfully changed password</response>
        /// <response code="404">No users found in db</response>
        /// <response code="500">Other server errors</response>
        [HttpPut("{id}/ChangePassword")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult ChangePassword(int id, [FromForm] ChangePasswordDto dto)
        {
            try
            {
                dto.UserId = id;
                _editUserPassword.Execute(dto);
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
        /// Edit user email address
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <response code="200">User successfully changed email</response>
        /// <response code="404">No users found in db</response>
        /// <response code="409">Email address already exists in db</response>
        /// <response code="500">Other server errors</response>
        [HttpPut("{id}/ChangeEmail")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult ChangeEmail(int id, [FromForm] ChangeEmailDto dto)
        {
            try
            {
                dto.UserId = id;
                _editUserEmail.Execute(dto);
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
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("Authenticate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Authenticate([FromForm] LogInInfoDto dto)
        {
            try
            {
                var user = _authUser.Execute(dto);
                return Ok(user);
            }
            catch (EntityNotFoundException)
            {
                return NotFound("Invalid login info");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
