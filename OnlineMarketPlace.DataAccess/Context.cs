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
        public string Host { get; set; }
        public string Db { get; set; }
        public string DbUser { get; set; }
        public string DbPassword { get; set; }

        //tables
        public DbSet<Users> Users { get; set; }
        public DbSet<ShippingAddresses> ShippingAddresses { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<SubCategories> SubCategories { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<Coupons> Coupons { get; set; }
        public DbSet<Shippers> Shippers { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<OrderCoupons> OrderCoupons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=" + Host + ";Initial Catalog=" + Db + ";Persist Security Info=True;User ID=" + DbUser + ";Password=" + DbPassword);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasQueryFilter(x => x.Active);
            modelBuilder.Entity<ShippingAddresses>().HasQueryFilter(x => x.Active);
            modelBuilder.Entity<Roles>().HasQueryFilter(x => x.Active);
            modelBuilder.Entity<Categories>().HasQueryFilter(x => x.Active);
            modelBuilder.Entity<SubCategories>().HasQueryFilter(x => x.Active);
            modelBuilder.Entity<Images>().HasQueryFilter(x => x.Active);
            modelBuilder.Entity<Products>().HasQueryFilter(x => x.Active);
            modelBuilder.Entity<ProductImages>().HasQueryFilter(x => x.Active);
            modelBuilder.Entity<Coupons>().HasQueryFilter(x => x.Active);
            modelBuilder.Entity<Shippers>().HasQueryFilter(x => x.Active);
            modelBuilder.Entity<Orders>().HasQueryFilter(x => x.Active);
            modelBuilder.Entity<OrderProducts>().HasQueryFilter(x => x.Active);
            modelBuilder.Entity<OrderCoupons>().HasQueryFilter(x => x.Active);
        }
        
        public Context(string host, string db, string dbUser, string dbPassword)
        {
            this.Host = host;
            this.Db = db;
            this.DbUser = dbUser;
            this.DbPassword = dbPassword;
        }

        public Context()
        {
            Host = "192.168.1.104";
            Db = "OnlineMarketPlace";
            DbUser = "SA";
            DbPassword = "Password1!";
        }
    }
}
