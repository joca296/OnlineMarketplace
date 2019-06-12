using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineMarketPlace.Application.Commands;
using OnlineMarketPlace.DataAccess;
using OnlineMarketPlace.EfCommands;

namespace OnlineMarketPlace.MVC
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Products}/{action=Index}/{id?}");
            });
        }
    }
}
