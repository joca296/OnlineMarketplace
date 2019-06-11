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
    public class EfEditUserPasswordCommand : EfCommand, IEditUserPasswordCommand
    {
        public EfEditUserPasswordCommand(Context context) : base(context)
        {
        }

        public void Execute(ChangePasswordDto request)
        {
            if (Validate(request))
            {
                var user = _context.Users.Find(request.UserId);

                user.DateUpdated = DateTime.Now;
                user.Password = Functions.CreateSha256Hash(request.NewPassword);

                _context.SaveChanges();
            }
        }

        public bool Validate(ChangePasswordDto request)
        {
            if (!_context.Users.Any(x => x.Id == request.UserId))
                throw new EntityNotFoundException($"User with id: {request.UserId}");

            return true;
        }
    }
}
