using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.DataTransfer;
using OnlineMarketPlace.Application.Exceptions;
using OnlineMarketPlace.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.EfCommands
{
    public class EfGetRolesCommand : EfCommand, IGetRolesCommand
    {
        public EfGetRolesCommand(Context context) : base(context)
        {
        }

        public IEnumerable<RoleDto> Execute(int? request)
        {
            List<RoleDto> roleDtos = new List<RoleDto>();
            if (request == null)
            {
                foreach (var role in _context.Roles)
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
                var role = _context.Roles.Find(request);
                if (role == null)
                    throw new EntityNotFoundException($"Role with id: {request}");
                else
                {
                    var roleDto = new RoleDto
                    {
                        Id = role.Id,
                        Name = role.Name
                    };
                    roleDtos.Add(roleDto);
                }

            }
            return roleDtos;
        }

        public bool Validate(int? request)
        {
            throw new NotImplementedException();
        }
    }
}
