using Microsoft.EntityFrameworkCore;
using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.Exceptions;
using OnlineMarketPlace.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace OnlineMarketPlace.EfCommands
{
    public class EfDeleteShipperCommand : EfCommand, IDeleteShipperCommand
    {
        public EfDeleteShipperCommand(Context context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var shippers = _context.Shippers
                .Include(s => s.Orders)
                    .ThenInclude(o => o.User)
                .Include(s => s.Orders)
                    .ThenInclude(o => o.OrderProducts)
                        .ThenInclude(op => op.Product)
                .Include(s => s.Orders)
                    .ThenInclude(o => o.OrderCoupons)
                .AsQueryable()
                .Where(s => s.Id == request);

            if (shippers.Count() != 1)
                throw new EntityNotFoundException($"Shipper with id: {request}");

            var shipper = shippers.First();

            foreach (var order in shipper.Orders)
            {
                if (order.DateShipped == null)
                {
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("onlinemarketplace@gmail.com", "OnlineMarketPlace");
                    message.To.Add(new MailAddress(order.User.Email, order.User.FirstName + " " + order.User.LastName));
                    message.Subject = "OnlineMarketPlace - Order cancelation";

                    string body = $"We would like to inform you that your order with id {order.Id} is cancelled.";
                    body += $" Reason: Shipper with id {shipper.Id} and name {shipper.Name} ";
                    body += $"is removed from our store and all orders made with this shipper is cancelled. ";
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

            shipper.Active = false;

            _context.SaveChanges();
        }

        public bool Validate(int request)
        {
            throw new NotImplementedException();
        }
    }
}
