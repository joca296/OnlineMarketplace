using Microsoft.EntityFrameworkCore;
using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.DataTransfer;
using OnlineMarketPlace.Application.Exceptions;
using OnlineMarketPlace.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineMarketPlace.EfCommands
{
    public class EfGetShippingAddressesCommand : EfCommand, IGetShippingAddressesCommand
    {
        public EfGetShippingAddressesCommand(Context context) : base(context)
        {
        }

        public IEnumerable<ShippingAddressDto> Execute(int request)
        {
            var shippingAddresses = _context.ShippingAddresses
                .Include(sa => sa.User)
                .AsQueryable()
                .Where(sa => sa.User.Id == request);

            if (shippingAddresses.Count() == 0)
                throw new EntityNotFoundException($"User with id: {request}");

            List<ShippingAddressDto> shippingAddressDtos = shippingAddresses.Select(sa => new ShippingAddressDto {
                ShippingAddressId = sa.Id,
                UserId = sa.User.Id,
                Country = sa.Country,
                State = sa.State,
                City = sa.City,
                Address = sa.Address,
                PostalCode = sa.PostalCode
            }).ToList();

            return shippingAddressDtos;
        }

        public bool Validate(int request)
        {
            throw new NotImplementedException();
        }
    }
}
