using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.DataTransfer;
using OnlineMarketPlace.DataAccess;
using OnlineMarketPlace.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.EfCommands
{
    public class EfCreateRoleCommand : ICreateRoleCommand
    {
        private readonly Context _context;

        public EfCreateRoleCommand(Context context)
        {
            _context = context;
        }

        public void Execute(CreateRoleDto request)
        {

            _context.Roles.Add(new Roles
            {
                Name = request.Name,
                DateCreated = DateTime.Now,
                Active = true
            });

            _context.SaveChanges();
        }
    }
}
