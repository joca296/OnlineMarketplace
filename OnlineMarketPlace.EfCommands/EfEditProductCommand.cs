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
    public class EfEditProductCommand : EfCommand, IEditProductCommand
    {
        public EfEditProductCommand(Context context) : base(context)
        {
        }

        public void Execute(CreateProductDto request)
        {
            if (Validate(request))
            {
                var product = _context.Products
                    .Include(p => p.ProductImages)
                        .ThenInclude(pi => pi.Image)
                    .AsQueryable()
                    .Where(x => x.Id == request.Id)
                    .First();
                
                product.Name = request.Name;
                product.Description = request.Description;
                product.UnitPrice = request.UnitPrice;
                product.UnitWeight = request.UnitWeight;
                product.QuantityAvailable = request.QuantityAvailable;
                product.Category = _context.Categories.Find(request.CategoryId);
                product.SubCategory = _context.SubCategories.Find(request.SubCategoryId);
                product.DateUpdated = DateTime.Now;

                if(request.ImagePaths.Count != 0)
                {
                    foreach (var productImage in product.ProductImages)
                    {
                        productImage.Image.Active = false;
                        productImage.Active = false;
                    }

                    int i = 0;
                    foreach (var imagePath in request.ImagePaths)
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
                }
                
                _context.SaveChanges();
            }
        }

        public bool Validate(CreateProductDto request)
        {
            if (!_context.Products.Any(x => x.Id == request.Id))
                throw new EntityNotFoundException($"Product with id: {request.Id}");

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
