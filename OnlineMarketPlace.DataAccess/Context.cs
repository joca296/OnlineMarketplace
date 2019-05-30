using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.DataAccess
{
    public class Context : DbContext
    {
        public static readonly Context _context = new Context("", "", "", "");

        private string host { get; set; }
        private string db { get; set; }
        private string dbUser { get; set; }
        private string dbPassword { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=" + host + ";Initial Catalog=" + db + ";Persist Security Info=True;User ID=" + dbUser + ";Password=" + dbPassword);
        }
        
        private Context(string host, string db, string dbUser, string dbPassword)
        {
            this.host = host;
            this.db = db;
            this.dbUser = dbUser;
            this.dbPassword = dbPassword;
        }
    }
}
