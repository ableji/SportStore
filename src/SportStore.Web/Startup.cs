using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SportStore.Service;
using SportStore.Data;
using Microsoft.EntityFrameworkCore;
using SportStore.Model.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using SportStore.Model.Validation;
using SportStore.Web.Infrastructure.Middlewares;

namespace SportStore.Web
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }


        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
                Configuration = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            

            //dependency injection for services
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IProvinceService, ProvinceService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IImageService, ImageService>();


            services.AddDbContext<ApplicationDBContext>(option =>
            option.UseSqlServer(Configuration["Data:SportStore:ConnectionStrings:DefaultConnection"]));

            services.AddDbContext<AppIdentityDBContext>(op =>
            op.UseSqlServer(Configuration["Data:SportStoreIdentity:ConnectionStrings:DefaultConnection"]));


            services.AddIdentity<AppUser, IdentityRole>(opt => {
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 5;
                opt.User.RequireUniqueEmail = true;
                 }
            ).AddEntityFrameworkStores<AppIdentityDBContext>();

            // Add framework services.
            services.AddMvc();

            //add validations here
            services.AddMvc().AddFluentValidation(fv =>
              fv.RegisterValidatorsFromAssemblyContaining<ProductValidator>());

            services.AddMvc().AddFluentValidation(fv =>
              fv.RegisterValidatorsFromAssemblyContaining<ProductCategoryValidator>());

            services.AddMvc().AddFluentValidation(fv =>
            fv.RegisterValidatorsFromAssemblyContaining<MessageValidator>());


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //My custom middleware
            app.UseMiddleware<BlockIEMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseMvc(route =>
            {
                route.MapRoute(name: "pagination", template: "Products/Page{page}", defaults: new { Controller = "Product", action = "Index" });
                route.MapAreaRoute(name:"AdminDefault",areaName:"Admin", template: "Admin/{Action=Index}/{id?}",defaults:new { Controller = "Admin" });
                route.MapRoute(name: "Default", template: "{Controller=Product}/{Action=Index}/{id?}");
            });


            SeedData.EnsurePopulated(app);
            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
