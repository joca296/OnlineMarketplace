using Microsoft.EntityFrameworkCore;
using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.Exceptions;
using OnlineMarketPlace.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineMarketPlace.EfCommands
{
    public class EfDeleteUserCommand : EfCommand, IDeleteUserCommand
    {
        public EfDeleteUserCommand(Context context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var users = _context.Users
                .Include(u => u.Orders)
                    .ThenInclude(o => o.OrderCoupons)
                .Include(u => u.Orders)
                    .ThenInclude(o => o.OrderProducts)
                        .ThenInclude(op => op.Product)
                .Include(u => u.ShippingAddresses)
                .AsQueryable()
                .Where(u => u.Id == request);

            if (users.Count() != 1)
                throw new EntityNotFoundException($"User with id {request}");

            var user = users.First();

            foreach (var shippingAddress in user.ShippingAddresses)
                shippingAddress.Active = false;

            if(user.Orders.Count != 0)
                foreach (var order in user.Orders)
                {
                    if (order.OrderCoupons.Count() != 0)
                        foreach (var coupon in order.OrderCoupons)
                            coupon.Active = false;

                    foreach (var product in order.OrderProducts)
                        product.Active = false;

                    if(order.DateShipped == null)
                        foreach (var product in order.OrderProducts)
                        {
                            product.Product.QuantityAvailable += product.Quantity;
                        }

                    order.Active = false;
                }

            user.Active = false;

            _context.SaveChanges();
        }

        public bool Validate(int request)
        {
            throw new NotImplementedException();
        }
    }
}
