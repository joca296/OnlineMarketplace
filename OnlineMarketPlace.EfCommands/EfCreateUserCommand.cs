using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.DataTransfer;
using OnlineMarketPlace.Application.Exceptions;
using OnlineMarketPlace.DataAccess;
using OnlineMarketPlace.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace OnlineMarketPlace.EfCommands
{
    public class EfCreateUserCommand : EfCommand, ICreateUserCommand
    {
        public EfCreateUserCommand(Context context) : base(context)
        {
        }

        public void Execute(CreateUserDto request)
        {
            if(_context.Roles.Find(request.RoleId) == null)
                throw new EntityNotFoundException("Role with id:" + request.RoleId);

            if (_context.Users.Any(x => x.Email == request.Email))
                throw new EntityAlreadyExists("Email: " + request.Email);

            string validationKey = Functions.CreateSha256Hash(Functions.GetUniqID());

            _context.Users.Add(new Users {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = Functions.CreateSha256Hash(request.Password),
                DateCreated = DateTime.Now,
                Active = false,
                Key = validationKey,
                Role = _context.Roles.Find(request.RoleId)
            });

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("onlinemarketplace@gmail.com", "OnlineMarketPlace");
            mailMessage.To.Add(new MailAddress(request.Email, request.FirstName + " " + request.LastName));
            mailMessage.Subject = "OnlineMarketPlace - Account activation";

            string message = "You have registered on our website, use the following link to activate your account."+System.Environment.NewLine;
            message += Functions.BaseUrl + "/api/Users/Activate/" + validationKey;
            mailMessage.Body = message;

            Functions.SmtpClient.Send(mailMessage);

            _context.SaveChanges();
        }
    }
}
