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
    public class EfCreateSubCategoryCommand : ICreateSubCategoryCommand
    {
        private readonly Context _context;

        public EfCreateSubCategoryCommand(Context context)
        {
            _context = context;
        }
        public void Execute(CreateSubCategoryDto request)
        {
            if(_context.Categories.Find(request.CategoryId) == null)
            {
                throw new EntityNotFoundException("Category with id: " + request.CategoryId);
            }

            _context.SubCategories.Add(new SubCategories
            {
                Active = true,
                DateCreated = DateTime.Now,
                Category = _context.Categories.Find(request.CategoryId),
                Name = request.Name
            });
            _context.SaveChanges();
        }
    }
}
