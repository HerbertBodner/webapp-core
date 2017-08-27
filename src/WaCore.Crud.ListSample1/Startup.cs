﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WaCore.Crud.ListSample1.Data;
using WaCore.Crud.ListSample1.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using WaCore.Crud.ListSample1.Services;

namespace WaCore.Crud.ListSample1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var connection = @"Server=(localdb)\mssqllocaldb;Database=WaCore.Sample;Trusted_Connection=True;";
            services.AddDbContext<LibraryDbContext>(optionsBuilder =>
                optionsBuilder.UseSqlServer(connection));

            services.AddUnitOfWork<LibraryDbContext, IUnitOfWork, UnitOfWork>(repoConfig =>
                repoConfig.AddRepository<IBooksListRepository, BookListRepository>());

            services.AddTransient<IBookListDataService, BookListDataService>();
            services.AddTransient<IBooksListRepository, BookListRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, LibraryDbContext dbContext)
        {
            DbInitializer.Initialize(dbContext);

            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}