using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.DataTransfer;
using OnlineMarketPlace.Application.Exceptions;
using OnlineMarketPlace.DataAccess;
using OnlineMarketPlace.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineMarketPlace.EfCommands
{
    public class EfCreateCategoryCommand : EfCommand, ICreateCategoryCommand
    {
        public EfCreateCategoryCommand(Context context) : base(context)
        {
        }

        public void Execute(CreateCategotyDto request)
        {
            if (Validate(request))
            {
                if (Validate(request))
                {
                    _context.Categories.Add(new Categories
                    {
                        Active = true,
                        DateCreated = DateTime.Now,
                        Name = request.Name
                    });
                    _context.SaveChanges();
                }
            }
        }

        public bool Validate(CreateCategotyDto request)
        {
            if (_context.Categories.Any(x => x.Name.Trim().ToLower() == request.Name.Trim().ToLower()))
                throw new EntityAlreadyExistsException("Category with name:" + request.Name);

            return true;
        }
    }
}
