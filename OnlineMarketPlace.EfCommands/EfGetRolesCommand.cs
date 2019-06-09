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
    public class EfGetRolesCommand : EfCommand, IGetRolesCommand
    {
        public EfGetRolesCommand(Context context) : base(context)
        {
        }

        public IEnumerable<RoleDto> Execute(NameSearch request)
        {
            List<RoleDto> roleDtos = new List<RoleDto>();
            
            if(request.Id == null)
            {
                var roles = _context.Roles.AsQueryable();

                if (request.Name != null)
                {
                    roles = roles.Where(x => x.Name.Trim().ToLower().Contains(request.Name.Trim().ToLower()));
                }

                foreach (var role in roles)
                {
                    var roleDto = new RoleDto
                    {
                        Id = role.Id,
                        Name = role.Name
                    };
                    roleDtos.Add(roleDto);
                }
            }
            else
            {
                var role = _context.Roles.Find(request.Id);
                if (role == null)
                    throw new EntityNotFoundException($"Role with id: {request.Id}");

                var roleDto = new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name
                };

                roleDtos.Add(roleDto);
            }

            return roleDtos;
        }

        public bool Validate(NameSearch request)
        {
            throw new NotImplementedException();
        }
    }
}
