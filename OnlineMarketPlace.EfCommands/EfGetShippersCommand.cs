﻿using OnlineMarketPlace.Application.Commands;
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
    public class EfGetShippersCommand : EfCommand, IGetShippersCommand
    {
        public EfGetShippersCommand(Context context) : base(context)
        {
        }
        
        public IEnumerable<ShipperDto> Execute(ShipperSearch request)
        {
            List<ShipperDto> shipperDtos = new List<ShipperDto>();

            if(request.Id == null)
            {
                var shippers = _context.Shippers.AsQueryable();

                if (request.Name != null)
                    shippers = shippers.Where(x => x.Name.Trim().ToLower().Contains(request.Name.Trim().ToLower()));

                if (request.MinFreightBase != null)
                    shippers = shippers.Where(x => x.FreightBase >= request.MinFreightBase);

                if (request.MaxFreightBase != null)
                    shippers = shippers.Where(x => x.FreightBase <= request.MaxFreightBase);

                if (request.MinFreightPerKilo != null)
                    shippers = shippers.Where(x => x.FreightPerKilo >= request.MinFreightPerKilo);

                if (request.MaxFreightBase != null)
                    shippers = shippers.Where(x => x.FreightPerKilo <= request.MaxFreightBase);

                foreach (var shipper in shippers)
                {
                    var shipperDto = new ShipperDto
                    {
                        Id = shipper.Id,
                        FreightPerKilo = shipper.FreightPerKilo,
                        FreightBase = shipper.FreightBase,
                        Name = shipper.Name
                    };
                    shipperDtos.Add(shipperDto);
                }
            }
            else
            {
                var shipper = _context.Shippers.Find(request.Id);
                if (shipper == null)
                    throw new EntityNotFoundException($"Shipper with id: {request.Id}");

                var shipperDto = new ShipperDto
                {
                    Id = shipper.Id,
                    FreightPerKilo = shipper.FreightPerKilo,
                    FreightBase = shipper.FreightBase,
                    Name = shipper.Name
                };
                shipperDtos.Add(shipperDto);
            }

            return shipperDtos;
        }

        public bool Validate(ShipperSearch request)
        {
            throw new NotImplementedException();
        }
    }
}
