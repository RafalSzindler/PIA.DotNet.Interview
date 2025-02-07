using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace PIA.DotNet.Interview.WebUI.Pages
{
    public class ReadmeModel : PageModel
    {
        private readonly ILogger<ReadmeModel> _logger;

        public void OnGet()
        {
            _logger.LogInformation("Data was loaded on the page Readme");
        }
    }
}
