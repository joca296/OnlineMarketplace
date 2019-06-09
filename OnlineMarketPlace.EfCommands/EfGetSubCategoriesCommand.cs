using Microsoft.EntityFrameworkCore;
using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.Application.DataTransfer;
using OnlineMarketPlace.Application.Exceptions;
using OnlineMarketPlace.Application.Searches;
using OnlineMarketPlace.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineMarketPlace.EfCommands
{
    public class EfGetSubCategoriesCommand : EfCommand, IGetSubCategoriesCommand
    {
        public EfGetSubCategoriesCommand(Context context) : base(context)
        {
        }

        public IEnumerable<SubCategoryDto> Execute(SubCategorySearch request)
        {
            List<SubCategoryDto> subCategoryDtos = new List<SubCategoryDto>();

            if (request.Id == null)
            {
                var subCategories = _context.SubCategories
                    .Include(sc => sc.Category)
                    .AsQueryable();

                if (request.Name != null)
                    subCategories = subCategories.Where(x => x.Name.Trim().ToLower().Contains(request.Name.Trim().ToLower()));

                if (request.CategoryId != null)
                    subCategories = subCategories.Where(x => x.Category.Id == request.CategoryId);

                foreach(var subCategory in subCategories)
                {
                    var subCategoryDto = new SubCategoryDto
                    {
                        Id = subCategory.Id,
                        Name = subCategory.Name,
                        CategoryId = subCategory.Category.Id
                    };

                    subCategoryDtos.Add(subCategoryDto);
                }
            }
            else
            {
                var subCategory = _context.SubCategories
                    .Include(sc=> sc.Category)
                    .AsQueryable()
                    .Where(x=> x.Id == request.Id)
                    .First();

                if (subCategory == null)
                    throw new EntityNotFoundException($"Subcategory with id: {request.Id}");

                var subCategoryDto = new SubCategoryDto
                {
                    Id = subCategory.Id,
                    Name = subCategory.Name,
                    CategoryId = subCategory.Category.Id
                };

                subCategoryDtos.Add(subCategoryDto);
            }

            return subCategoryDtos;
        }

        public bool Validate(SubCategorySearch request)
        {
            throw new NotImplementedException();
        }
    }
}
