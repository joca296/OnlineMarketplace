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
    public class EfAuthenticateUserCommand : EfCommand, IAuthenticateUserCommand
    {
        public EfAuthenticateUserCommand(Context context) : base(context)
        {
        }

        public GetUserBasicDto Execute(LogInInfoDto request)
        {
            if (Validate(request))
            {
                var user = _context.Users
                    .Include(u => u.Role)
                    .AsQueryable()
                    .Where(u => u.Email == request.Email)
                    .Where(u => u.Password == Functions.CreateSha256Hash(request.Password))
                    .First();

                return new GetUserBasicDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    RoleName = user.Role.Name
                };
            }
            else
                return null;
        }

        public bool Validate(LogInInfoDto request)
        {
            if (!_context.Users.Any(x => x.Email == request.Email && x.Password == Functions.CreateSha256Hash(request.Password)))
                throw new EntityNotFoundException();

            return true;
        }
    }
}
