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
        private readonly IGetCategoriesCommand _getCategories;

        public CategoriesController(ICreateCategoryCommand createCategory, IGetCategoriesCommand getCategories)
        {
            _createCategory = createCategory;
            _getCategories = getCategories;
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
        public IActionResult Post([FromForm] CategoryDto dto)
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

        /// <summary>
        /// Get category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Category with given id
        /// </returns>
        /// <response code="200">Successfully returned category</response>
        /// <response code="404">Category with given id not found</response>
        /// <response code="500">Other server errors</response>
        // GET: api/Category/id
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Get(int id)
        {
            try
            {
                var results = _getCategories.Execute(id);
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
        /// Get categories
        /// </summary>
        /// <returns>
        /// List containing all categories in db if id is NULL, 
        /// </returns>
        /// <response code="500">Other server errors</response>
        // GET: api/Category
        [HttpGet()]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult GetAll()
        {
            try
            {
                var results = _getCategories.Execute(null);
                return Ok(results);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
