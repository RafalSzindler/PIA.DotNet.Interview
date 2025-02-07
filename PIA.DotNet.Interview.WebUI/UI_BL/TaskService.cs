using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PIA.DotNet.Interview.Core.Logging;
using PIA.DotNet.Interview.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;


namespace PIA.DotNet.Interview.WebUI.UI_BL
{
    public class TaskService : ITaskService
    {
        private readonly HttpClient _httpClient;
        private string _remoteServiceBaseUrl =string.Empty; // as default value
        
        private readonly PIA.DotNet.Interview.Core.Logging.ILogger logger = new CrudLogger();

        public TaskService()
        {
            _httpClient = new HttpClient();
            logger.LogCreate(this.ToString(), "TaskService was created");
        }
        public TaskService(string remoteServiceBaseUrl)
        {
            _remoteServiceBaseUrl = remoteServiceBaseUrl;
            _httpClient = new HttpClient();
            logger.LogCreate(this.ToString(), "TaskService was created with the _remoteServiceBaseURl");
        }

        public async Task<bool> Add(TaskViewModel task)
        {
            logger.LogCreate(this.ToString(), "TaskService Add method is used");
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(string id, TaskViewModel task)
        {
            logger.LogCreate(this.ToString(), "TaskService Delete method is used");
            throw new NotImplementedException();
        }

        public Task<bool> Edit(string id, TaskViewModel task)
        {
            logger.LogCreate(this.ToString(), "TaskService Edit method is used");

            var requestJson = JsonConvert.SerializeObject(task);
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            
            var responseresult = _httpClient.PostAsync(String.Format("{0}api/task/EditTask?id={1}", _remoteServiceBaseUrl,id), content);

            return Task.FromResult(true);              
                       
        }
        public Task<bool> Set(string id, TaskViewModel task)
        {
            logger.LogCreate(this.ToString(), "TaskService Set method is used");
            // to do
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TaskViewModel>> Get()
        {
            logger.LogCreate(this.ToString(), "TaskService Get Collection method is used");
            try
            {
                var responseString = await _httpClient.GetStringAsync(String.Format("{0}api/task/GetTasks", _remoteServiceBaseUrl));
                return JsonConvert.DeserializeObject<IEnumerable<TaskViewModel>>(responseString);
            }
            catch (Exception)
            {
                // example for logging... 
                // to do task_3
                return new List<TaskViewModel>();
            }
        }

        public string OpenPopup()
        {
            return "<h1> This Is Modeless Popup Window</h1>";
        }

        public async Task<TaskViewModel> Get(string id)
        {
            logger.LogCreate(this.ToString(), "TaskService Get entity method is used");

            var requestJson = JsonConvert.SerializeObject(id);
            var responseString = await _httpClient.GetStringAsync(String.Format("{0}api/task/GetTask?id={1}", _remoteServiceBaseUrl,id));
            return JsonConvert.DeserializeObject<TaskViewModel>(responseString);
        }
    }
}
