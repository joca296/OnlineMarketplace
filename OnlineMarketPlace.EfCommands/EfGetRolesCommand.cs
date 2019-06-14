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
            var roles = _context.Roles.AsQueryable();

            if (request.Id == null)
            {
                if (request.Name != null)
                    roles = roles.Where(x => x.Name.Trim().ToLower().Contains(request.Name.Trim().ToLower()));
            }
            else
                roles = roles.Where(x=> x.Id == request.Id);

            if (roles.Count() == 0)
                throw new EntityNotFoundException($"Roles");

            foreach (var role in roles)
            {
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
