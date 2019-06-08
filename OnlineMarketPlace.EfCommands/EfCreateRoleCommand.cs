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
    public class EfCreateRoleCommand : EfCommand, ICreateRoleCommand
    {
        public EfCreateRoleCommand(Context context) : base(context)
        {
        }

        public void Execute(RoleDto request)
        {
            if (Validate(request))
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

        public bool Validate(RoleDto request)
        {
            if (_context.Roles.Any(x => x.Name.Trim().ToLower() == request.Name.Trim().ToLower()))
                throw new EntityAlreadyExistsException("Role with name:" + request.Name);

            return true;
        }
    }
}
