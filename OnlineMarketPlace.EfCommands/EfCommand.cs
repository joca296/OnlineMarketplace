using OnlineMarketPlace.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.EfCommands
{
    public class EfCommand
    {
        protected readonly Context _context;

        public EfCommand(Context context)
        {
            _context = context;
        }
    }
}
