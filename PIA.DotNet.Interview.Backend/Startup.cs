using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PIA.DotNet.Interview.Backend.Service;
using PIA.DotNet.Interview.Core.Database;
using PIA.DotNet.Interview.Core.Logging;
using PIA.DotNet.Interview.Core.Repositories;
using System.IO;

namespace PIA.DotNet.Interview.Backend
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public static IConfiguration Configuration { get; private set; }
        public void ConfigureServices(IServiceCollection services)
        {
            //Configuration
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            string _dbPath = Configuration.GetSection("AppSettings").GetValue<string>("DBPath");

            // core
            services.AddSingleton<IDbContext, DbContext>(provider => new DbContext(_dbPath));
            services.AddSingleton<ITaskRepository, TaskRepository>();
            services.AddSingleton<PIA.DotNet.Interview.Core.Logging.ILogger, CrudLogger>(); 

            // service
            services.AddSingleton<ITaskLogicService, TaskLogicService>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PIA.DotNet.Interview",
                    Version = "v1",
                    Description = "PIA.DotNet.Interview API",
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PIA.DotNet.Interview API");

                c.RoutePrefix = string.Empty;
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
