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
    public class EfCreateShipperCommand : EfCommand, ICreateShipperCommand
    {
        public EfCreateShipperCommand(Context context) : base(context)
        {
        }

        public void Execute(ShipperDto request)
        {
            if (Validate(request))
            {
                _context.Shippers.Add(new Shippers
                {
                    Active = true,
                    DateCreated = DateTime.Now,
                    Name = request.Name,
                    FreightBase = request.FreightBase,
                    FreightPerKilo = request.FreightPerKilo
                });

                _context.SaveChanges();
            }
        }

        public bool Validate(ShipperDto request)
        {
            if (_context.Shippers.Any(x => x.Name.Trim().ToLower() == request.Name.Trim().ToLower()))
                throw new EntityAlreadyExistsException($"Shipper with name: {request.Name}");

            return true;
        }
    }
}
