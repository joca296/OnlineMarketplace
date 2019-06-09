using OnlineMarketPlace.Application.DataTransfer;
using OnlineMarketPlace.Application.Interfaces;
using OnlineMarketPlace.Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Application.Commands
{
    public interface IGetSubCategoriesCommand : ICommand<SubCategorySearch, IEnumerable<SubCategoryDto>>
    {
    }
}
