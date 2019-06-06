using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.Exceptions;
using OnlineMarketPlace.DataAccess;
using OnlineMarketPlace.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineMarketPlace.EfCommands
{
    public class EfActiaveUserCommand : EfCommand, IActivateUserCommand
    {
        public EfActiaveUserCommand(Context context) : base(context)
        {
        }

        public void Execute(string request)
        {
            if (Validate(request))
            {
                var user = _context.Users.First(x => x.Key == request);

                user.Active = true;
                user.DateUpdated = DateTime.Now;
                user.Key = null;

                _context.SaveChanges();
            }
        }

        public bool Validate(string request)
        {
            if (!_context.Users.Any(x => x.Key == request))
                throw new EntityNotFoundException("User with key: " + request);

            return true;
        }
    }
}
