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
    public class EfGetUsersCommand : EfCommand, IGetUsersCommand
    {
        public EfGetUsersCommand(Context context) : base(context)
        {
        }

        public IEnumerable<GetUserDto> Execute(UserSearch request)
        {
            var users = _context.Users
                .Include(u => u.ShippingAddresses)
                .Include(u => u.Role)
                .Include(u => u.Orders)
                    .ThenInclude(o => o.OrderProducts)
                        .ThenInclude(od => od.Product)
                            .ThenInclude(p => p.SubCategory)
                                .ThenInclude(sc => sc.Category)
                .Include(u => u.Orders)
                    .ThenInclude(o => o.Shipper)
                .AsQueryable();

            if (request.Id != null)
                users = users.Where(u => u.Id == request.Id);
            else
            {
                if (request.FirstName != null)
                    users = users.Where(u => u.FirstName.Trim().ToLower().Contains(request.FirstName.Trim().ToLower()));

                if (request.LastName != null)
                    users = users.Where(u => u.LastName.Trim().ToLower().Contains(request.LastName.Trim().ToLower()));

                if (request.Email != null)
                    users = users.Where(u => u.Email.Equals(request.Email));
            }

            if (users == null)
                throw new EntityNotFoundException("Users");

            List<GetUserDto> userDtos = users.Select(u => new GetUserDto {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                DateRegistered = u.DateCreated,
                Role = new RoleDto
                {
                    Id = u.Role.Id,
                    Name = u.Role.Name
                },
                ShippingAddresses = u.ShippingAddresses.Select(sa => new ShippingAddressDto {
                    UserId = sa.User.Id,
                    Country = sa.Country,
                    State = sa.State,
                    City = sa.City,
                    Address = sa.Address,
                    PostalCode = sa.PostalCode
                }).ToList(),
                Orders = u.Orders.Select(o => new GetOrderDto
                {
                    TotalPrice = o.TotalPrice,
                    TotalFreight = o.TotalFreight,
                    Shipper = new ShipperDto
                    {
                        Id = o.Shipper.Id,
                        Name = o.Shipper.Name,
                        FreightBase = o.Shipper.FreightBase,
                        FreightPerKilo = o.Shipper.FreightPerKilo
                    },
                    ShippingAddress = new ShippingAddressDto
                    {
                        UserId = o.ShippingAddress.User.Id,
                        Country = o.ShippingAddress.Country,
                        State = o.ShippingAddress.State,
                        City = o.ShippingAddress.City,
                        Address = o.ShippingAddress.Address,
                        PostalCode = o.ShippingAddress.PostalCode
                    },
                    Products = o.OrderProducts.Select(op=> new OrderProductsDto {
                        ProductId = op.Product.Id,
                        ProductName = op.Product.Name,
                        QuantityOrdered = op.Quantity,
                        UnitPrice = op.Price,
                        UnitWeight = op.Product.UnitWeight,
                        CategoryName = op.Product.Category.Name,
                        SubCategoryName = op.Product.SubCategory.Name
                    }).ToList(),
                    DateOrdered = o.DateCreated,
                    DateShipped = o.DateShipped,
                    DateDelivered = o.DateDelivered
                }).ToList()
            }).ToList();

            return userDtos;
        }

        public bool Validate(UserSearch request)
        {
            throw new NotImplementedException();
        }
    }
}
