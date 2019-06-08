using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.DataTransfer;
using OnlineMarketPlace.Application.Exceptions;
using OnlineMarketPlace.Application.Interfaces;
using OnlineMarketPlace.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.EfCommands
{
    public class EfGetCategoriesCommand : EfCommand, IGetCategoriesCommand
    {
        public EfGetCategoriesCommand(Context context) : base(context)
        {
        }

        public IEnumerable<CategoryDto> Execute(int? request)
        {
            List<CategoryDto> categoryDtos = new List<CategoryDto>();
            if (request == null)
            {
                foreach (var category in _context.Categories)
                {
                    var categoryDto = new CategoryDto
                    {
                        Id = category.Id,
                        Name = category.Name
                    };
                    categoryDtos.Add(categoryDto);
                }
            }
            else
            {
                var category = _context.Categories.Find(request);
                if (category == null)
                    throw new EntityNotFoundException($"Category with id: {request}");
                else
                {
                    var categoryDto = new CategoryDto
                    {
                        Id = category.Id,
                        Name = category.Name
                    };
                    categoryDtos.Add(categoryDto);
                }

            }
            return categoryDtos;
        }

        public bool Validate(int? request)
        {
            throw new NotImplementedException();
        }
    }
}
