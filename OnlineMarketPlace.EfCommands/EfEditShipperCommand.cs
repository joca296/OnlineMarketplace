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
    public class EfEditShipperCommand : EfCommand, IEditShipperCommand
    {
        public EfEditShipperCommand(Context context) : base(context)
        {
        }

        public void Execute(ShipperDto request)
        {
            if (Validate(request))
            {
                var shipper = _context.Shippers.Find(request.Id);

                shipper.Name = request.Name;
                shipper.FreightBase = request.FreightBase;
                shipper.FreightPerKilo = request.FreightPerKilo;
                shipper.DateUpdated = DateTime.Now;

                _context.SaveChanges();
            }
        }

        public bool Validate(ShipperDto request)
        {
            if (!_context.Shippers.Any(x => x.Id == request.Id))
                throw new EntityNotFoundException($"Shipper with id: {request.Name}");

            if (_context.Shippers.Any(x => x.Name.Trim().ToLower() == request.Name.Trim().ToLower() && x.Id != request.Id))
                throw new EntityAlreadyExistsException($"Shipper with name: {request.Name}");

            return true;
        }
    }
}
