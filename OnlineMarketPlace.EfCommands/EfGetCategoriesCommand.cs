using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.DataTransfer;
using OnlineMarketPlace.Application.Exceptions;
using OnlineMarketPlace.Application.Interfaces;
using OnlineMarketPlace.Application.Searches;
using OnlineMarketPlace.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineMarketPlace.EfCommands
{
    public class EfGetCategoriesCommand : EfCommand, IGetCategoriesCommand
    {
        public EfGetCategoriesCommand(Context context) : base(context)
        {
        }

        public IEnumerable<CategoryDto> Execute(NameSearch request)
        {
            List<CategoryDto> categoryDtos = new List<CategoryDto>();
            var categories = _context.Categories.AsQueryable();

            if (request.Id == null)
            {
                if (request.Name != null)
                    categories = categories.Where(x => x.Name.Trim().ToLower().Contains(request.Name.Trim().ToLower()));
            }
            else
                categories = categories.Where(x=> x.Id == request.Id);

            if (categories.Count() == 0)
                throw new EntityNotFoundException($"Categories");

            foreach (var category in categories)
            {
                var categoryDto = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name
                };
                categoryDtos.Add(categoryDto);
            }

            return categoryDtos;
        }

        public bool Validate(NameSearch request)
        {
            throw new NotImplementedException();
        }
    }
}
