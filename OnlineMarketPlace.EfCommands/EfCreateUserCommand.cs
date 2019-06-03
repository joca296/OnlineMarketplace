using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.DataTransfer;
using OnlineMarketPlace.Application.Exceptions;
using OnlineMarketPlace.DataAccess;
using OnlineMarketPlace.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.EfCommands
{
    public class EfCreateUserCommand : ICreateUserCommand
    {
        private readonly Context _context;

        public EfCreateUserCommand(Context context)
        {
            _context = context;
        }

        public void Execute(CreateUserDto request)
        {
            if(_context.Roles.Find(request.RoleId) == null)
                throw new EntityNotFoundException("Role with id:" + request.RoleId);

            _context.Users.Add(new Users {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = Functions.CreateSha256Hash(request.Password),
                DateCreated = DateTime.Now,
                Active = false,
                Role = _context.Roles.Find(request.RoleId)
            });

            _context.SaveChanges();

            //add user activation here
        }
    }
}
