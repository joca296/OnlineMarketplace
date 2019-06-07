using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Application.Exceptions
{
    public class ProductNotAvailableException : Exception
    {
        public ProductNotAvailableException()
        {

        }

        public ProductNotAvailableException(int productId, int quantAvailable, int quantRequested) :
            base($"Product with id: {productId} doesn't have enough units for this order. Requested: {quantRequested}, Available: {quantAvailable}")
        {

        }
    }
}
