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
    public class SubCategoriesController : ControllerBase
    {
        private readonly ICreateSubCategoryCommand _createSubCategory;
        private readonly IGetSubCategoriesCommand _getSubCategories;

        public SubCategoriesController(ICreateSubCategoryCommand createSubCategory, IGetSubCategoriesCommand getSubCategories)
        {
            _createSubCategory = createSubCategory;
            _getSubCategories = getSubCategories;
        }

        /// <summary>
        /// Creates a new subcategory
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <response code="200">Subcategory successfully added to the database</response>
        /// <response code="404">Given category ID doesn't exist in the database</response>
        /// <response code="409">Subcategory with given name already AND category ID already exists in the database</response>
        /// <response code="500">Other server errors</response>
        // POST: api/SubCategories
        [HttpPost]
        public IActionResult Post([FromForm] SubCategoryDto dto)
        {
            try
            {
                _createSubCategory.Execute(dto);
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

        /// <summary>
        /// Get subcategory by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Subcategory with given id
        /// </returns>
        /// <response code="200">Successfully returned subcategory</response>
        /// <response code="404">Subcategory with given id not found</response>
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
                var search = new SubCategorySearch
                {
                    Id = id
                };
                var result = _getSubCategories.Execute(search);
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
        /// Get subcategories
        /// </summary>
        /// <returns></returns>
        /// <response code="200">List containing all subcategories with given search criteria</response>
        /// <response code="500">Other server errors</response>
        // GET: api/Category
        [HttpGet()]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult GetAll([FromQuery] SubCategorySearch search)
        {
            try
            {
                var results = _getSubCategories.Execute(search);
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
