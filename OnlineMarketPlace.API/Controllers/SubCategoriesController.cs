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
    public class SubCategoriesController : ControllerBase
    {
        private readonly ICreateSubCategoryCommand _createSubCategory;

        public SubCategoriesController(ICreateSubCategoryCommand createSubCategory)
        {
            _createSubCategory = createSubCategory;
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
    }
}
