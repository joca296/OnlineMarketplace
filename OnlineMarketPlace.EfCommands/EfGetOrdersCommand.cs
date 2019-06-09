using Microsoft.EntityFrameworkCore;
using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.DataTransfer;
using OnlineMarketPlace.Application.Exceptions;
using OnlineMarketPlace.Application.Searches;
using OnlineMarketPlace.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineMarketPlace.EfCommands
{
    public class EfGetOrdersCommand : EfCommand, IGetOrdersCommand
    {
        public EfGetOrdersCommand(Context context) : base(context)
        {
        }

        public IEnumerable<GetOrderDto> Execute(OrderSearch request)
        {
            var orders = _context.Orders
                .Include(o => o.Shipper)
                .Include(o => o.ShippingAddress)
                .Include(o => o.User)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                        .ThenInclude(p => p.SubCategory)
                            .ThenInclude(sc => sc.Category)
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

            List<GetOrderDto> getOrderDtos = orders.Select(o => new GetOrderDto {
                FirstName = o.User.FirstName,
                LastName = o.User.LastName,
                UserEmail = o.User.Email,
                TotalPrice = o.TotalPrice,
                TotalFreight = o.TotalFreight,
                DateOrdered = o.DateCreated,
                DateShipped = o.DateShipped,
                DateDelivered = o.DateDelivered,
                ShippingAddress = new ShippingAddressDto
                {
                    UserId = o.ShippingAddress.User.Id,
                    Country = o.ShippingAddress.Country,
                    State = o.ShippingAddress.State,
                    City = o.ShippingAddress.City,
                    Address = o.ShippingAddress.Address,
                    PostalCode = o.ShippingAddress.PostalCode
                },
                Shipper = new ShipperDto
                {
                    Id = o.Shipper.Id,
                    Name = o.Shipper.Name,
                    FreightBase = o.Shipper.FreightBase,
                    FreightPerKilo = o.Shipper.FreightPerKilo
                },
                Products = o.OrderProducts.Select(op => new OrderProductsDto {
                    ProductId = op.Product.Id,
                    ProductName = op.Product.Name,
                    QuantityOrdered = op.Quantity,
                    UnitPrice = op.Price,
                    UnitWeight = op.Product.UnitWeight,
                    CategoryName = op.Product.Category.Name,
                    SubCategoryName = op.Product.SubCategory.Name
                }).ToList()
            }).ToList();

            return getOrderDtos;
        }

        public bool Validate(OrderSearch request)
        {
            throw new NotImplementedException();
        }
    }
}
