using Microsoft.EntityFrameworkCore;
using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.Exceptions;
using OnlineMarketPlace.Application.Searches;
using OnlineMarketPlace.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace OnlineMarketPlace.EfCommands
{
    public class EfDeleteOrdersCommand : EfCommand, IDeleteOrdersCommand
    {
        public EfDeleteOrdersCommand(Context context) : base(context)
        {
        }

        public void Execute(OrderSearch request)
        {
            var orders = _context.Orders
                .Include(o => o.OrderCoupons)
                .Include(o => o.Shipper)
                .Include(o => o.User)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .AsQueryable();

            if (request.Id == null)
            {
                if (request.UserId != null)
                    orders = orders.Where(o => o.User.Id == request.UserId);

                if (request.FirstName != null)
                    orders = orders.Where(o => o.User.FirstName.Trim().ToLower().Contains(request.FirstName.Trim().ToLower()));

                if (request.LastName != null)
                    orders = orders.Where(o => o.User.LastName.Trim().ToLower().Contains(request.LastName.Trim().ToLower()));

                if (request.MinTotalPrice != null)
                    orders = orders.Where(o => o.TotalPrice >= request.MinTotalPrice);

                if (request.MaxTotalPrice != null)
                    orders = orders.Where(o => o.TotalPrice <= request.MaxTotalPrice);

                if (request.ShipperId != null)
                    orders = orders.Where(o => o.Shipper.Id == request.ShipperId);

                if (request.ShipperName != null)
                    orders = orders.Where(o => o.Shipper.Name.Trim().ToLower().Contains(request.ShipperName.Trim().ToLower()));

                if (request.Shipped != null)
                {
                    if (request.Shipped == true)
                        orders = orders.Where(o => o.DateShipped != null);
                    else
                        orders = orders.Where(o => o.DateShipped == null);
                }

                if (request.Delivered != null)
                {
                    if (request.Delivered == true)
                        orders = orders.Where(o => o.DateDelivered != null);
                    else
                        orders = orders.Where(o => o.DateDelivered == null);
                }

                if (request.MinDateOrdered != null)
                    orders = orders.Where(o => o.DateCreated >= request.MinDateOrdered);

                if (request.MaxDateOrdered != null)
                    orders = orders.Where(o => o.DateCreated <= request.MaxDateOrdered);

                if (request.MinDateShipped != null)
                    orders = orders.Where(o => o.DateShipped >= request.MinDateShipped);

                if (request.MaxDateShipped != null)
                    orders = orders.Where(o => o.DateShipped <= request.MaxDateShipped);

                if (request.MinDateDelivered != null)
                    orders = orders.Where(o => o.DateDelivered >= request.MinDateDelivered);

                if (request.MaxDateDelivered != null)
                    orders = orders.Where(o => o.DateDelivered <= request.MaxDateDelivered);
            }
            else
                orders = orders.Where(o => o.Id == request.Id);

            if (orders.Count() == 0)
                throw new EntityNotFoundException("Orders");

            foreach (var order in orders)
            {
                if (order.DateShipped == null)
                {
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("onlinemarketplace@gmail.com", "OnlineMarketPlace");
                    message.To.Add(new MailAddress(order.User.Email, order.User.FirstName + " " + order.User.LastName));
                    message.Subject = "OnlineMarketPlace - Order cancelation";

                    string body = $"We would like to inform you that your order with id {order.Id} is cancelled.";
                    body += "Reason: Either you or someone else cancelled the order. If it's the former,";
                    body += "https://www.youtube.com/watch?v=fSfsWFUmaX8";
                    message.Body = body;

                    Functions.SmtpClient.Send(message);

                    foreach (var product in order.OrderProducts)
                    {
                        product.Product.QuantityAvailable += product.Quantity;
                    }
                }

                if (order.OrderCoupons.Count() != 0)
                    foreach (var coupon in order.OrderCoupons)
                        coupon.Active = false;

                foreach (var product in order.OrderProducts)
                    product.Active = false;

                order.Active = false;
            }

            _context.SaveChanges();
        }

        public bool Validate(OrderSearch request)
        {
            throw new NotImplementedException();
        }
    }
}
