using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.DataTransfer;
using OnlineMarketPlace.DataAccess;
using OnlineMarketPlace.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.EfCommands
{
    public class EfCreateCategoryCommand : ICreateCategoryCommand
    {
        private readonly Context _context;

        public EfCreateCategoryCommand(Context context)
        {
            _context = context;
        }

        public void Execute(CreateCategotyDto request)
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
