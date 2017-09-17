using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WaCore.Sample.Data;
using Microsoft.EntityFrameworkCore;
using WaCore.Sample.Data.Repositories;

namespace WaCore.Sample
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
            services.AddMvc();

            var connection = @"Server=(localdb)\mssqllocaldb;Database=WaCore.Sample;Trusted_Connection=True;";
            services.AddDbContext<LibraryDbContext>(optionsBuilder => 
                optionsBuilder.UseSqlServer(connection));

            services.AddUnitOfWork<LibraryDbContext, IUnitOfWork, UnitOfWork>(repoConfig =>
                repoConfig.AddRepositoriesFromAssemblyOf<UnitOfWork>());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, LibraryDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            DbInitializer.Initialize(dbContext);
        }
    }
}
