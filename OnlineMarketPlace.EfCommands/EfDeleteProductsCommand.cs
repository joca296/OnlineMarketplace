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
    public class EfDeleteProductsCommand : EfCommand, IDeleteProductsCommand
    {
        public EfDeleteProductsCommand(Context context) : base(context)
        {
        }

        public void Execute(ProductSearch request)
        {
            var products = _context.Products
                .Include(p => p.ProductImages)
                    .ThenInclude(pi => pi.Image)
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .AsQueryable();

            if (request.Id == null)
            {
                if (request.Name != null)
                    products = products.Where(p => p.Name.Trim().ToLower().Contains(request.Name.Trim().ToLower()));

                if (request.QuantityAvailable != null)
                    products = products.Where(p => p.QuantityAvailable >= request.QuantityAvailable);

                if (request.MinUnitPrice != null)
                    products = products.Where(p => p.UnitPrice >= request.MinUnitPrice);

                if (request.MaxUnitPrice != null)
                    products = products.Where(p => p.UnitPrice <= request.MaxUnitPrice);

                if (request.CategoryId != null)
                    products = products.Where(p => p.Category.Id == request.CategoryId);

                if (request.SubCategoryId != null)
                    products = products.Where(p => p.SubCategory.Id == request.SubCategoryId);

                if (request.CategoryName != null)
                    products = products.Where(p => p.Category.Name.Trim().ToLower().Contains(request.CategoryName.Trim().ToLower()));

                if (request.SubCategoryName != null)
                    products = products.Where(p => p.SubCategory.Name.Trim().ToLower().Contains(request.SubCategoryName.Trim().ToLower()));
            }
            else
                products = products.Where(p => p.Id == request.Id);

            if (products.Count() == 0)
                throw new EntityNotFoundException("Products");

            var orderProducts = _context.OrderProducts
                .Include(op => op.Product)
                .Include(op => op.Order)
                    .ThenInclude(o => o.OrderCoupons)
                .Include(op => op.Order)
                    .ThenInclude(o => o.User)
                .AsQueryable();

            foreach(var product in products)
            {
                var orders = orderProducts.Where(op => op.Product.Id == product.Id).Where(op => op.Order.DateShipped == null);
                foreach(var order in orders)
                {
                    var coupons = order.Order.OrderCoupons;
                    if(coupons.Count() != 0)
                        foreach (var coupon in coupons)
                            coupon.Active = false;

                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("onlinemarketplace@gmail.com", "OnlineMarketPlace");
                    message.To.Add(new MailAddress(order.Order.User.Email, order.Order.User.FirstName + " " + order.Order.User.LastName));
                    message.Subject = "OnlineMarketPlace - Order cancelation";

                    string body = $"We would like to inform you that your order with id {order.Order.Id} is cancelled.";
                    body += $" Reason: Product with id {order.Product.Id} and name {order.Product.Name} ";
                    body += $"is removed from our store and all orders containing this product is canceled. ";
                    body += "We are sorry for any inconviniences caused.";
                    message.Body = body;

                    Functions.SmtpClient.Send(message);

                    order.Order.Active = false;
                    order.Active = false;
                }

                var productImages = product.ProductImages;
                foreach (var productImage in productImages)
                {
                    productImage.Image.Active = false;
                    productImage.Active = false;
                }
                product.Active = false;
            }

            _context.SaveChanges();
        }

        public bool Validate(ProductSearch request)
        {
            throw new NotImplementedException();
        }
    }
}
