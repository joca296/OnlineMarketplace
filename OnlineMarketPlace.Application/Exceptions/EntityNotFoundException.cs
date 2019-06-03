using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
        {

        }

        public EntityNotFoundException(string entity)
            : base (entity+" doesn't exist")
        {

        }
    }
}
