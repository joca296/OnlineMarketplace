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
    public class CategoriesController : ControllerBase
    {
        private readonly ICreateCategoryCommand _createCategory;

        public CategoriesController(ICreateCategoryCommand createCategory)
        {
            _createCategory = createCategory;
        }

        // POST: api/Category
        [HttpPost]
        public IActionResult Post([FromForm] CreateCategotyDto dto)
        {
            try
            {
                _createCategory.Execute(dto);
                return Ok();
            }
            catch (Exception e)
            {
                return UnprocessableEntity(e.Message);
            }
        }
    }
}
