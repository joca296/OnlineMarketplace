using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Application.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException()
        {

        }

        public EntityAlreadyExistsException(string entity) 
            : base (entity+" already exists.")
        {

        }
    }
}
