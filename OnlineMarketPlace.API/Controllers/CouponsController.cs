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
    public class CouponsController : ControllerBase
    {
        private readonly ICreateCouponCommand _createCoupon;

        public CouponsController(ICreateCouponCommand createCoupon)
        {
            _createCoupon = createCoupon;
        }

        /// <summary>
        /// Create a new coupon
        /// </summary>
        /// <param name="dto"></param>
        /// <response code="200">Coupon has been successfully added to the database</response>
        /// <response code="409">Coupon with given code already exists</response>
        /// <response code="500">Other server errors</response>
        /// <returns></returns>
        // POST: api/Coupons
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromForm] CouponDto dto)
        {
            try
            {
                _createCoupon.Execute(dto);
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
