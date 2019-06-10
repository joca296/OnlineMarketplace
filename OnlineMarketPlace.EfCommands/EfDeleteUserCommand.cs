using Microsoft.EntityFrameworkCore;
using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.Exceptions;
using OnlineMarketPlace.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineMarketPlace.EfCommands
{
    public class EfDeleteUserCommand : EfCommand, IDeleteUserCommand
    {
        public EfDeleteUserCommand(Context context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var users = _context.Users
                .Include(u => u.Orders)
                .Include(u => u.ShippingAddresses)
                .AsQueryable()
                .Where(u => u.Id == request);

            if (users.Count() != 1)
                throw new EntityNotFoundException($"User with id {request}");

            var user = users.First();

            foreach (var shippingAddress in user.ShippingAddresses)
                shippingAddress.Active = false;

            foreach (var order in user.Orders)
                order.Active = false;

            user.Active = false;

            _context.SaveChanges();
        }

        public bool Validate(int request)
        {
            throw new NotImplementedException();
        }
    }
}
