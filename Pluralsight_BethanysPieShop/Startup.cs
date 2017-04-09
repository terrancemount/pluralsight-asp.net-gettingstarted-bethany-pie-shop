using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pluralsight_BethanysPieShop.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Pluralsight_BethanysPieShop
{
    public class Startup
    {
        private IConfigurationRoot _configurationRoot;

        public Startup(IHostingEnvironment hostingEnviroment)
        {
            _configurationRoot = new ConfigurationBuilder()  //creates a new builder so I can add dynamic configuation stuff to it
                .SetBasePath(hostingEnviroment.ContentRootPath) //set path to root of project
                .AddJsonFile("appsettings.json") //add the Db connection string in the appsetting.json file in the root of project
                .Build();  //build the configuration file
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        
            services.AddDbContext<AppDbContext>(options =>
                                        options.UseSqlServer(_configurationRoot.GetConnectionString("DefaultConnection")));

            //adds a loose connection to the between the pie and category models and the controller.
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IPieRepository, PieRepository>();

            services.AddMemoryCache();
            services.AddSession();
            services.AddMvc();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            //add code to stetup basic http pipeline
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.UseSession();

            DbInitializer.Seed(app);
        }
    }
}
