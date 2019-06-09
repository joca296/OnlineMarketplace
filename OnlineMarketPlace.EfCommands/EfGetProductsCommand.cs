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
    public class EfGetProductsCommand : EfCommand, IGetProductsCommand
    {
        public EfGetProductsCommand(Context context) : base(context)
        {
        }

        public IEnumerable<GetProductDto> Execute(ProductSearch request)
        {
            var products = _context.Products
                .Include(p => p.ProductImages)
                    .ThenInclude(pi => pi.Image)
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .AsQueryable();

            if (request.Id == null)
            {
                if (request.Name != null)
                    products = products.Where(p => p.Name.Trim().ToLower().Contains(request.Name.Trim().ToLower()));

                if (request.QuantityAvailable != null)
                    products = products.Where(p => p.QuantityAvailable >= request.QuantityAvailable);

                if (request.MinUnitPrice != null)
                    products = products.Where(p => p.UnitPrice >= request.MinUnitPrice);

                if (request.MaxUnitPrice != null)
                    products = products.Where(p => p.UnitPrice <= request.MaxUnitPrice);

                if (request.CategoryId != null)
                    products = products.Where(p => p.Category.Id == request.CategoryId);

                if (request.SubCategoryId != null)
                    products = products.Where(p => p.SubCategory.Id == request.SubCategoryId);

                if (request.CategoryName != null)
                    products = products.Where(p => p.Category.Name.Trim().ToLower().Contains(request.CategoryName.Trim().ToLower()));

                if (request.SubCategoryName != null)
                    products = products.Where(p => p.SubCategory.Name.Trim().ToLower().Contains(request.SubCategoryName.Trim().ToLower()));
            }
            else
                products = products.Where(p => p.Id == request.Id);

            if (products.Count() == 0)
                throw new EntityNotFoundException("Products");

            List<GetProductDto> productDtos = products.Select(p => new GetProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                UnitPrice = p.UnitPrice,
                UnitWeight = p.UnitWeight,
                QuantityAvailable = p.QuantityAvailable,
                Category = new CategoryDto
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name
                },
                SubCategory = new SubCategoryDto
                {
                    Id = p.SubCategory.Id,
                    Name = p.SubCategory.Name,
                    CategoryId = p.SubCategory.Category.Id
                },
                ImagePaths = p.ProductImages.Select(pi => pi.Image.Path).ToList(),
                ImageAlts = p.ProductImages.Select(pi => pi.Image.Alt).ToList()
            }).ToList();

            return productDtos;
        }

        public bool Validate(ProductSearch request)
        {
            throw new NotImplementedException();
        }
    }
}
