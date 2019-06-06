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
    public class EfCreateShippingAddressCommand : EfCommand, ICreateShippingAddressCommand
    {
        public EfCreateShippingAddressCommand(Context context) : base(context)
        {
        }

        public void Execute(ShippingAddressDto request)
        {
            if (Validate(request))
            {
                var newShippingAddress = new ShippingAddresses
                {
                    Active = true,
                    DateCreated = DateTime.Now,
                    User = _context.Users.Find(request.UserId),
                    Country = request.Country,
                    State = request.State,
                    City = request.City,
                    Address = request.Address,
                    PostalCode = request.PostalCode
                };

                _context.ShippingAddresses.Add(newShippingAddress);

                _context.SaveChanges();
            }
        }

        public bool Validate(ShippingAddressDto request)
        {
            if (!_context.Users.Any(x => x.Id == request.UserId))
                throw new EntityNotFoundException("User with id: " + request.UserId);

            
            if 
            (
                _context.ShippingAddresses.Any
                (
                    x=>
                    x.Country == request.Country &&
                    x.State == request.State &&
                    x.City == request.City &&
                    x.Address == request.Address &&
                    x.PostalCode == request.PostalCode &&
                    x.User.Id == request.UserId
                )
            )
                throw new EntityAlreadyExistsException("Shipping address");


            return true;
        }
    }
}
