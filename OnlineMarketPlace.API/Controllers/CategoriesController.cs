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
    public class CategoriesController : ControllerBase
    {
        private readonly ICreateCategoryCommand _createCategory;

        public CategoriesController(ICreateCategoryCommand createCategory)
        {
            _createCategory = createCategory;
        }

        /// <summary>
        /// Creates a new category
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <response code="200">Category successfully added to the database</response>
        /// <response code="409">Category with given name already exists in the database</response>
        /// <response code="500">Other server errors</response>
        // POST: api/Category
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromForm] CreateCategotyDto dto)
        {
            try
            {
                _createCategory.Execute(dto);
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
