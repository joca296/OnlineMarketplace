using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Application.Helpers
{
    public class Paginator<T> where T : class
    {
        public static List<List<T>> Paginate(List<T> items, int itemsPerPage)
        {
            int numberOfPages = (int)Math.Ceiling(items.Count / (double)itemsPerPage);

            List<List<T>> paginatedResult = new List<List<T>>();

            int i = 0, page = 0;
            foreach(var item in items)
            {
                if (i == 0)
                    paginatedResult.Add(new List<T>());

                paginatedResult[page].Add(item);
                i++;

                if (i == itemsPerPage)
                {
                    i = 0;
                    page++;
                }
            }

            return paginatedResult;
        }

        
    }
}
