﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineMarketPlace.DataAccess;

namespace OnlineMarketPlace.DataAccess.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20190601072342_OnlineMarketPlace-v2")]
    partial class OnlineMarketPlacev2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OnlineMarketPlace.Domain.Tables.Categories", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<DateTime>("dateCreated");

                    b.Property<DateTime?>("dateUpdated");

                    b.Property<string>("key");

                    b.Property<string>("name");

                    b.HasKey("id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("OnlineMarketPlace.Domain.Tables.Images", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Productsid");

                    b.Property<bool>("active");

                    b.Property<string>("alt");

                    b.Property<DateTime>("dateCreated");

                    b.Property<DateTime?>("dateUpdated");

                    b.Property<string>("key");

                    b.Property<string>("path");

                    b.HasKey("id");

                    b.HasIndex("Productsid");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("OnlineMarketPlace.Domain.Tables.Products", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<int?>("categoryid");

                    b.Property<DateTime>("dateCreated");

                    b.Property<DateTime?>("dateUpdated");

                    b.Property<string>("description");

                    b.Property<string>("key");

                    b.Property<string>("name");

                    b.Property<int>("quantityAvailable");

                    b.Property<double>("unitPrice");

                    b.HasKey("id");

                    b.HasIndex("categoryid");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("OnlineMarketPlace.Domain.Tables.Roles", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<DateTime>("dateCreated");

                    b.Property<DateTime?>("dateUpdated");

                    b.Property<string>("key");

                    b.Property<string>("name");

                    b.HasKey("id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("OnlineMarketPlace.Domain.Tables.ShippingAddresses", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<DateTime>("dateCreated");

                    b.Property<DateTime?>("dateUpdated");

                    b.Property<string>("key");

                    b.Property<int?>("userid");

                    b.HasKey("id");

                    b.HasIndex("userid");

                    b.ToTable("ShippingAddresses");
                });

            modelBuilder.Entity("OnlineMarketPlace.Domain.Tables.Users", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<DateTime>("dateCreated");

                    b.Property<DateTime?>("dateUpdated");

                    b.Property<string>("eMail");

                    b.Property<string>("firstName");

                    b.Property<string>("key");

                    b.Property<string>("lastName");

                    b.Property<string>("password");

                    b.Property<int?>("roleid");

                    b.HasKey("id");

                    b.HasIndex("roleid");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OnlineMarketPlace.Domain.Tables.Images", b =>
                {
                    b.HasOne("OnlineMarketPlace.Domain.Tables.Products")
                        .WithMany("images")
                        .HasForeignKey("Productsid");
                });

            modelBuilder.Entity("OnlineMarketPlace.Domain.Tables.Products", b =>
                {
                    b.HasOne("OnlineMarketPlace.Domain.Tables.Categories", "category")
                        .WithMany()
                        .HasForeignKey("categoryid");
                });

            modelBuilder.Entity("OnlineMarketPlace.Domain.Tables.ShippingAddresses", b =>
                {
                    b.HasOne("OnlineMarketPlace.Domain.Tables.Users", "user")
                        .WithMany("shippingAddresses")
                        .HasForeignKey("userid");
                });

            modelBuilder.Entity("OnlineMarketPlace.Domain.Tables.Users", b =>
                {
                    b.HasOne("OnlineMarketPlace.Domain.Tables.Roles", "role")
                        .WithMany()
                        .HasForeignKey("roleid");
                });
#pragma warning restore 612, 618
        }
    }
}
