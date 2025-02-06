﻿using Newtonsoft.Json;
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
        private readonly string _remoteServiceBaseUrl = "http://localhost:5001/"; // to do task_4

        public TaskService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<bool> Add(TaskViewModel task)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(string id, TaskViewModel task)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Edit(string id, TaskViewModel task)
        {
            var requestJson = JsonConvert.SerializeObject(task);
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            
            var responseresult = _httpClient.PostAsync(String.Format("{0}api/task/EditTask?id={1}", _remoteServiceBaseUrl,id), content);

            return Task.FromResult(true);              
                       
        }
        public Task<bool> Set(string id, TaskViewModel task)
        {
            // to do
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TaskViewModel>> Get()
        {
             
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
            var requestJson = JsonConvert.SerializeObject(id);
            var responseString = await _httpClient.GetStringAsync(String.Format("{0}api/task/GetTask?id={1}", _remoteServiceBaseUrl,id));
            return JsonConvert.DeserializeObject<TaskViewModel>(responseString);
        }
    }
}
