using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PIA.DotNet.Interview.Backend.Service;
using PIA.DotNet.Interview.Core.Database;
using PIA.DotNet.Interview.Core.Repositories;

namespace PIA.DotNet.Interview.Backend
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // core
            services.AddSingleton<IDbContext, DbContext>();
            services.AddSingleton<ITaskRepository, TaskRepository>();

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
