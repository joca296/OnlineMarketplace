using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Application.Exceptions
{
    public class EntityMissmatchException : Exception
    {
        public EntityMissmatchException()
        {

        }

        public EntityMissmatchException(string entity1, string entity2)
            : base($"{entity1} and {entity2} don't match to eachother")
        {

        }
    }
}
