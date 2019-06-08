using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.DataTransfer;
using OnlineMarketPlace.Application.Exceptions;
using OnlineMarketPlace.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.EfCommands
{
    public class EfGetShippersCommand : EfCommand, IGetShippersCommand
    {
        public EfGetShippersCommand(Context context) : base(context)
        {
        }
        
        public IEnumerable<ShipperDto> Execute(int? request)
        {
            List<ShipperDto> shipperDtos = new List<ShipperDto>();
            if (request == null)
            {
                foreach(var shipper in _context.Shippers)
                {
                    var shipperDto = new ShipperDto
                    {
                        Id = shipper.Id,
                        Name = shipper.Name,
                        FreightPerKilo = shipper.FreightPerKilo,
                        FreightBase = shipper.FreightBase
                    };
                    shipperDtos.Add(shipperDto);
                }
            }
            else
            {
                var shipper = _context.Shippers.Find(request);
                if (shipper == null)
                    throw new EntityNotFoundException($"Shipper with id: {request}");
                else
                {
                    var shipperDto = new ShipperDto
                    {
                        Id = shipper.Id,
                        Name = shipper.Name,
                        FreightPerKilo = shipper.FreightPerKilo,
                        FreightBase = shipper.FreightBase
                    };
                    shipperDtos.Add(shipperDto);
                }
                    
            }
            return shipperDtos;
        }

        public bool Validate(int? request)
        {
            throw new NotImplementedException();
        }
    }
}
