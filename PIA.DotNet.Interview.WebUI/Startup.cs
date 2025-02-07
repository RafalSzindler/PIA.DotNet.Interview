using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PIA.DotNet.Interview.Core.Database;
using PIA.DotNet.Interview.Core.Logging;
using PIA.DotNet.Interview.Core.Repositories;
using PIA.DotNet.Interview.WebUI.UI_BL;


namespace PIA.DotNet.Interview.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Configuration
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            string _dbPath = Configuration.GetSection("AppSettings").GetValue<string>("DBPath");

            string _remoteServiceBaseUrl = Configuration.GetSection("AppSettings").GetValue<string>("WebserviceHostName");


            // core
            services.AddSingleton<IDbContext, DbContext>(provider=> new DbContext(_dbPath));
            services.AddSingleton<ITaskRepository, TaskRepository>();
            services.AddSingleton<ILogger, CrudLogger>();

            // services
            services.AddTransient<TaskService>(provider => new TaskService(_remoteServiceBaseUrl));
            services.AddTransient<MarkdownService>();

            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
