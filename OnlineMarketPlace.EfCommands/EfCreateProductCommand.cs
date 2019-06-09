using Microsoft.EntityFrameworkCore;
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
    public class EfCreateProductCommand : EfCommand, ICreateProductCommand
    {
        public EfCreateProductCommand(Context context) : base(context)
        {
        }

        public void Execute(CreateProductDto request)
        {
            if (Validate(request))
            {
                var product = new Products
                {
                    Active = true,
                    DateCreated = DateTime.Now,
                    Name = request.Name,
                    Description = request.Description,
                    UnitPrice = request.UnitPrice,
                    UnitWeight = request.UnitWeight,
                    QuantityAvailable = request.QuantityAvailable,
                    Category = _context.Categories.Find(request.CategoryId),
                    SubCategory = _context.SubCategories.Find(request.SubCategoryId)
                };

                int i = 0;
                foreach(var imagePath in request.ImagePaths)
                {
                    var image = new Images
                    {
                        Active = true,
                        DateCreated = DateTime.Now,
                        Path = imagePath,
                        Alt = request.ImageAlts[i],
                    };

                    _context.ProductImages.Add(new ProductImages
                    {
                        Active = true,
                        DateCreated = DateTime.Now,
                        Product = product,
                        Image = image
                    });

                    i++;
                }

                _context.SaveChanges();
            }
        }

        public bool Validate(CreateProductDto request)
        {
            if (!_context.Categories.Any(x => x.Id == request.CategoryId))
                throw new EntityNotFoundException($"Category with id: {request.CategoryId}");

            if (!_context.SubCategories.Any(x => x.Id == request.SubCategoryId))
                throw new EntityNotFoundException($"Subcategory with id: {request.SubCategoryId}");

            var y = _context.SubCategories
                .Include(sc => sc.Category)
                .AsQueryable()
                .Where(sc => sc.Id == request.SubCategoryId && sc.Category.Id == request.CategoryId);
            if (y.Count() == 0)
                throw new EntityMissmatchException($"Subcategory with id: {request.SubCategoryId}", $"Category with id: {request.CategoryId}");

            return true;
        }
    }
}
