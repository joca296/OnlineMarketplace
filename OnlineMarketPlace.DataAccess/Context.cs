using Microsoft.EntityFrameworkCore;
using OnlineMarketPlace.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.DataAccess
{
    public class Context : DbContext
    {
        //database params
        public string host { get; set; }
        public string db { get; set; }
        public string dbUser { get; set; }
        public string dbPassword { get; set; }

        //tables
        public DbSet<Users> Users { get; set; }
        public DbSet<ShippingAddresses> ShippingAddresses { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Products> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=" + host + ";Initial Catalog=" + db + ";Persist Security Info=True;User ID=" + dbUser + ";Password=" + dbPassword);
        }
        
        public Context(string host, string db, string dbUser, string dbPassword)
        {
            this.host = host;
            this.db = db;
            this.dbUser = dbUser;
            this.dbPassword = dbPassword;
        }

        public Context()
        {
            host = "192.168.1.104";
            db = "OnlineMarketPlace";
            dbUser = "SA";
            dbPassword = "Password1!";
        }
    }
}
