using Microsoft.EntityFrameworkCore;
using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.DataTransfer;
using OnlineMarketPlace.Application.Exceptions;
using OnlineMarketPlace.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace OnlineMarketPlace.EfCommands
{
    public class EfDeleteShippingAddressCommand : EfCommand, IDeleteShippingAddressCommand
    {
        public EfDeleteShippingAddressCommand(Context context) : base(context)
        {
        }

        public void Execute(DeleteShippingAddressDto request)
        {
            if (Validate(request))
            {
                var shippingAddress = _context.ShippingAddresses
                    .Include(sa => sa.User)
                    .Include(sa => sa.Orders)
                        .ThenInclude(o => o.OrderCoupons)
                    .Include(sa => sa.Orders)
                        .ThenInclude(o => o.OrderProducts)
                            .ThenInclude(op => op.Product)
                    .AsQueryable()
                    .Where(sa => sa.Id == request.ShippingAddressId)
                    .First();

                if (shippingAddress.Orders.Count() != 0)
                    foreach (var order in shippingAddress.Orders)
                    {
                        if (order.DateShipped == null)
                        {
                            MailMessage message = new MailMessage();
                            message.From = new MailAddress("onlinemarketplace@gmail.com", "OnlineMarketPlace");
                            message.To.Add(new MailAddress(order.User.Email, order.User.FirstName + " " + order.User.LastName));
                            message.Subject = "OnlineMarketPlace - Order cancelation";

                            string body = $"We would like to inform you that your order with id {order.Id} is cancelled. ";
                            body += $"Reason: Shipping address for this order has been deleted and for safety, we have cancelled the order. ";
                            body += "We are sorry for any inconviniences caused.";
                            message.Body = body;

                            Functions.SmtpClient.Send(message);

                            foreach (var product in order.OrderProducts)
                            {
                                product.Product.QuantityAvailable += product.Quantity;
                            }
                        }

                        foreach (var product in order.OrderProducts)
                            product.Active = false;

                        if (order.OrderCoupons.Count() == 0)
                            foreach (var coupon in order.OrderCoupons)
                                coupon.Active = false;

                        order.Active = false;
                    }

                shippingAddress.Active = false;

                _context.SaveChanges();
            }
        }

        public bool Validate(DeleteShippingAddressDto request)
        {
            if (_context.ShippingAddresses.Any(x => x.Id == request.ShippingAddressId))
                throw new EntityNotFoundException($"Shipping address with id {request.ShippingAddressId}");

            if (_context.Users.Any(x => x.Id == request.UserId))
                throw new EntityNotFoundException($"User with id {request.ShippingAddressId}");

            if (_context.ShippingAddresses
                .Include(sa => sa.User)
                .AsQueryable()
                .Where(sa => sa.Id == request.ShippingAddressId)
                .Where(sa => sa.User.Id == request.UserId)
                .Count()
                == 0)
                throw new EntityMissmatchException($"Shipping address with id {request.ShippingAddressId}", $"User with id {request.ShippingAddressId}");

            return true;
        }
    }
}
