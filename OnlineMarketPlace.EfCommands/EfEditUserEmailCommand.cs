using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.DataTransfer;
using OnlineMarketPlace.Application.Exceptions;
using OnlineMarketPlace.DataAccess;

namespace OnlineMarketPlace.EfCommands
{
    public class EfEditUserEmailCommand : EfCommand, IEditUserEmailCommand
    {
        public EfEditUserEmailCommand(Context context) : base(context)
        {
        }

        public void Execute(ChangeEmailDto request)
        {
            if (Validate(request))
            {
                var user = _context.Users.Find(request.UserId);

                user.DateUpdated = DateTime.Now;
                user.Email = request.NewEmail;

                _context.SaveChanges();
            }
        }

        public bool Validate(ChangeEmailDto request)
        {
            if (!_context.Users.Any(x => x.Id == request.UserId))
                throw new EntityNotFoundException($"User with id: {request.UserId}");

            if (_context.Users.Any(x => x.Email == request.NewEmail))
                throw new EntityAlreadyExistsException($"Email address");

            return true;
        }
    }
}
