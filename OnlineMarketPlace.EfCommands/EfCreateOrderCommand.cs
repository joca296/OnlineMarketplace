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
    public class EfCreateOrderCommand : EfCommand, ICreateOrderCommand
    {
        public EfCreateOrderCommand(Context context) : base(context)
        {
        }

        public void Execute(CreateOrderDto request)
        {
            if (Validate(request))
            {
                double totalPrice = 0, totalFreight = 0;

                bool freeShipping = false;
                foreach (var couponCode in request.CouponCodes)
                {
                    if (_context.Coupons.First(x=> Functions.CreateSha256Hash(x.Code) == couponCode).FreeShipping)
                    {
                        freeShipping = true;
                        break;
                    }
                }

                if (!freeShipping)
                    totalFreight = _context.Shippers.Find(request.ShipperId).FreightBase.GetValueOrDefault(0);

                int i = 0;
                foreach(var productId in request.ProductIds)
                {
                    totalPrice += _context.Products.Find(productId).UnitPrice * request.QuantityPerProduct[i];
                    if (!freeShipping)
                        totalFreight +=
                            _context.Products.Find(productId).UnitWeight *
                            request.QuantityPerProduct[i] *
                            _context.Shippers.Find(request.ShipperId).FreightPerKilo;
                    i++;
                }

                foreach (var couponCode in request.CouponCodes)
                {
                    double discount = _context.Coupons.First(x => Functions.CreateSha256Hash(x.Code) == couponCode).Discount;
                    totalPrice -= totalPrice * (discount / 100);
                }

                var newOrder = new Orders
                {
                    Active = true,
                    DateCreated = DateTime.Now,
                    User = _context.Users.Find(request.UserId),
                    Shipper = _context.Shippers.Find(request.ShipperId),
                    ShippingAddress = _context.ShippingAddresses.Find(request.ShippingAddressId),
                    TotalPrice = totalPrice,
                    TotalFreight = totalFreight
                };

                foreach (var couponCode in request.CouponCodes)
                    _context.OrderCoupons.Add(new OrderCoupons
                    {
                        Active = true,
                        DateCreated = DateTime.Now,
                        Coupon = _context.Coupons.First(x => x.Code == couponCode),
                        Order = newOrder
                    });

                int j = 0;
                foreach (var productId in request.ProductIds)
                {
                    _context.OrderProducts.Add(new OrderProducts
                    {
                        Active = true,
                        DateCreated = DateTime.Now,
                        Product = _context.Products.Find(productId),
                        Order = newOrder,
                        Quantity = request.QuantityPerProduct[j],
                        Price = _context.Products.Find(productId).UnitPrice
                    });

                    var product = _context.Products.Find(productId);
                    product.QuantityAvailable -= request.QuantityPerProduct[j];

                    j++;
                }

                _context.SaveChanges();
            }
        }

        public bool Validate(CreateOrderDto request)
        {
            if (_context.Users.Find(request.UserId) == null)
                throw new EntityNotFoundException($"User with id: {request.UserId}");

            var shippingAddress = _context.ShippingAddresses.Find(request.ShippingAddressId);
            if (shippingAddress == null)
                throw new EntityNotFoundException($"Shipping address with id: {request.ShippingAddressId}");

            if (shippingAddress.User.Id != request.UserId)
                throw new EntityMissmatchException($"Shipping address with id: {request.ShippingAddressId}", $"User with id: {request.UserId}");

            if (request.ProductIds.Count != request.QuantityPerProduct.Count)
                throw new EntityMissmatchException("Product count", "Quantity per product");

            int i = 0;
            foreach (var productId in request.ProductIds)
            {
                var product = _context.Products.Find(productId);

                if (product == null)
                    throw new EntityNotFoundException($"Product with id: {productId}");

                if (product.QuantityAvailable < request.QuantityPerProduct[i])
                    throw new ProductNotAvailableException(productId, product.QuantityAvailable, request.QuantityPerProduct[i]);

                i++;
            }
                

            foreach (var couponCode in request.CouponCodes)
                if (_context.Coupons.FirstOrDefault(x=> Functions.CreateSha256Hash(x.Code) == couponCode) == null)
                    throw new EntityNotFoundException($"Coupon with code: {couponCode}");

            return true;
        }
    }
}
