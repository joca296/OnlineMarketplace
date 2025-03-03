﻿using Microsoft.EntityFrameworkCore;
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
    public class EfCreateSubCategoryCommand : EfCommand, ICreateSubCategoryCommand
    {
        public EfCreateSubCategoryCommand(Context context) : base(context)
        {
        }

        public void Execute(SubCategoryDto request)
        {
            

            _context.SubCategories.Add(new SubCategories
            {
                Active = true,
                DateCreated = DateTime.Now,
                Category = _context.Categories.Find(request.CategoryId),
                Name = request.Name
            });
            _context.SaveChanges();
        }

        public bool Validate(SubCategoryDto request)
        {
            if (_context.Categories.Find(request.CategoryId) == null)
                throw new EntityNotFoundException("Category with id: " + request.CategoryId);

            if (_context.SubCategories
                .Include(sc => sc.Category)
                .AsQueryable()
                .Where
                    (
                        x => 
                        (x.Name.Trim().ToLower() == request.Name.Trim().ToLower()) && 
                        (x.Category.Id == request.CategoryId)
                    )
                .Count()
                != 0
            )
                throw new EntityAlreadyExistsException("Sub Category with name:" + request.Name);

            return true;
        }
    }
}
