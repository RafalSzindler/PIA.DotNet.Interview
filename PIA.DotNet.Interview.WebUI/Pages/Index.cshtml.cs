using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Server.IIS.Core;
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

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public  ActionResult OnPost(string id)
        {
            var data = id;
            
            TaskService taskService = new TaskService();
            TaskViewModel taskViewModel = taskService.Get(id).Result;
            if (taskViewModel != null)
            {
                taskViewModel.IsFinished = true;
            }

            var result =taskService.Edit(id,taskViewModel);

            return Page();


        }

    }
}
