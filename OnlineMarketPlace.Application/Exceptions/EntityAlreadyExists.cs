using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Application.Exceptions
{
    public class EntityAlreadyExists : Exception
    {
        public EntityAlreadyExists()
        {

        }

        public EntityAlreadyExists(string entity) 
            : base (entity+" already exists.")
        {

        }
    }
}
