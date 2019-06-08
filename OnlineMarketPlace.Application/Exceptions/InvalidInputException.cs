using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Application.Exceptions
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException()
        {

        }

        public InvalidInputException(string message) : base (message)
        {

        }
    }
}
