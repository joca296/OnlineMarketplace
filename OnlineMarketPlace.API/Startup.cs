﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OnlineMarketPlace.Application;
using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.DataAccess;
using OnlineMarketPlace.EfCommands;
using Swashbuckle.AspNetCore.Swagger;

namespace OnlineMarketPlace.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<Context>();
            services.AddTransient<IActivateUserCommand, EfActiaveUserCommand>();
            services.AddTransient<IAuthenticateUserCommand, EfAuthenticateUserCommand>();
            services.AddTransient<ICreateCategoryCommand, EfCreateCategoryCommand>();
            services.AddTransient<ICreateCouponCommand, EfCreateCouponCommand>();
            services.AddTransient<ICreateOrderCommand, EfCreateOrderCommand>();
            services.AddTransient<ICreateProductCommand, EfCreateProductCommand>();
            services.AddTransient<ICreateRoleCommand, EfCreateRoleCommand>();
            services.AddTransient<ICreateShipperCommand, EfCreateShipperCommand>();
            services.AddTransient<ICreateShippingAddressCommand, EfCreateShippingAddressCommand>();
            services.AddTransient<ICreateSubCategoryCommand, EfCreateSubCategoryCommand>();
            services.AddTransient<ICreateUserCommand, EfCreateUserCommand>();
            services.AddTransient<IDeleteOrdersCommand, EfDeleteOrdersCommand>();
            services.AddTransient<IDeleteProductsCommand, EfDeleteProductsCommand>();
            services.AddTransient<IDeleteShipperCommand, EfDeleteShipperCommand>();
            services.AddTransient<IDeleteShippingAddressCommand, EfDeleteShippingAddressCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<IEditProductCommand, EfEditProductCommand>();
            services.AddTransient<IEditShipperCommand, EfEditShipperCommand>();
            services.AddTransient<IEditShippingAddressCommand, EfEditShippingAddressesCommand>();
            services.AddTransient<IEditUserEmailCommand, EfEditUserEmailCommand>();
            services.AddTransient<IEditUserPasswordCommand, EfEditUserPasswordCommand>();
            services.AddTransient<IGetCategoriesCommand, EfGetCategoriesCommand>();
            services.AddTransient<IGetOrdersCommand, EfGetOrdersCommand>();
            services.AddTransient<IGetProductsCommand, EfGetProductsCommand>();
            services.AddTransient<IGetRolesCommand, EfGetRolesCommand>();
            services.AddTransient<IGetShippersCommand, EfGetShippersCommand>();
            services.AddTransient<IGetShippingAddressesCommand, EfGetShippingAddressesCommand>();
            services.AddTransient<IGetSubCategoriesCommand, EfGetSubCategoriesCommand>();
            services.AddTransient<IGetUsersCommand, EfGetUsersCommand>();
            
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "OnlineMarketPlace API",
                    Version = "v1"
                });

                c.DocumentFilter<ModelsDocumentFilter>();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                //include model doc
                c.IncludeXmlComments(Path.Combine(ModelDocumentation.BasePath, ModelDocumentation.XmlFile));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineMarketPlace API v1");
                c.RoutePrefix = string.Empty;
            });
            
            app.UseMvc();
        }
    }
}
