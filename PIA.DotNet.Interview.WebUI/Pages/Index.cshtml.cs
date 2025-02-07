using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PIA.DotNet.Interview.Core.Models;
using PIA.DotNet.Interview.WebUI.UI_BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PIA.DotNet.Interview.WebUI.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;

        public IConfiguration Configuration { get; private set; }

        private string _remoteServiceBaseUrl = ""; // as default value

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            //Configuration
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            _remoteServiceBaseUrl = Configuration.GetSection("AppSettings").GetValue<string>("WebserviceHostName");
        }

        public void OnGet()
        {
            _logger.LogInformation("Data was loaded on the page");
        }

        public  ActionResult OnPostSubmit(string id)
        {
            var data = id;

            _logger.LogInformation(String.Format("Task  with ID - {0} was finished", id));
            TaskService taskService = new TaskService(_remoteServiceBaseUrl);
            TaskViewModel taskViewModel = taskService.Get(id).Result;
            if (taskViewModel != null)
            {
                taskViewModel.IsFinished = true;
            }

            var result =taskService.Edit(id,taskViewModel);
            _logger.LogInformation("Task changes  - Data was saved in to DB");
            return new JsonResult(new { success = true });


        }
        public ActionResult OnPostDelete(string id)
        {
            var data = id;

            _logger.LogInformation(String.Format("Task  with ID - {0}  will  be deleted", id));
            TaskService taskService = new TaskService(_remoteServiceBaseUrl);
            TaskViewModel taskViewModel = taskService.Get(id).Result;
            var result = taskService.Delete(id, taskViewModel);
            _logger.LogInformation("Task changes  - Data was deleted from to DB");
            
            return new JsonResult(new { success = true });

        }

    }
}
