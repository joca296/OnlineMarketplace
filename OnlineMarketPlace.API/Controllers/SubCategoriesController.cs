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
    public class SubCategoriesController : ControllerBase
    {
        private readonly ICreateSubCategoryCommand _createSubCategory;

        public SubCategoriesController(ICreateSubCategoryCommand createSubCategory)
        {
            _createSubCategory = createSubCategory;
        }

        // POST: api/SubCategories
        [HttpPost]
        public IActionResult Post([FromForm] CreateSubCategoryDto dto)
        {
            try
            {
                _createSubCategory.Execute(dto);
                return Ok();
            }
            catch (Exception e)
            {
                return UnprocessableEntity(e.Message);
            }
        }
    }
}
