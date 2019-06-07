using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.DataTransfer;
using OnlineMarketPlace.Application.Exceptions;
using OnlineMarketPlace.DataAccess;
using OnlineMarketPlace.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineMarketPlace.EfCommands
{
    public class EfCreateCouponCommand : EfCommand, ICreateCouponCommand
    {
        public EfCreateCouponCommand(Context context) : base(context)
        {
        }

        public void Execute(CouponDto request)
        {
            if (Validate(request))
            {
                _context.Coupons.Add(new Coupons
                {
                    Active = true,
                    DateCreated = DateTime.Now,
                    Name = request.Name,
                    Code = Functions.CreateSha256Hash(request.Code),
                    Discount = request.Discount,
                    FreeShipping = request.FreeShipping
                });

                _context.SaveChanges();
            }
        }

        public bool Validate(CouponDto request)
        {
            if (_context.Coupons.Any(x => x.Code == Functions.CreateSha256Hash(request.Code)))
                throw new EntityAlreadyExistsException();

            return true;
        }
    }
}
