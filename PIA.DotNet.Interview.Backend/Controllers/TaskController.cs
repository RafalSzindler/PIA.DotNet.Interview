using Microsoft.AspNetCore.Mvc;
using PIA.DotNet.Interview.Backend.Service;
using PIA.DotNet.Interview.Core.Logging;
using PIA.DotNet.Interview.Core.Models;
using System.Collections.Generic;

namespace PIA.DotNet.Interview.Backend.Controllers
{
   

    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private readonly PIA.DotNet.Interview.Core.Logging.ILogger logger = new CrudLogger();

        private readonly ITaskLogicService _taskLogicService;

        public TaskController(ITaskLogicService taskLogic)
        {
            _taskLogicService = taskLogic;
        }

        [HttpGet("[action]")]
        public IEnumerable<TaskViewModel> GetTasks()
        {
            logger.LogCreate(this.ToString(), "TaskController Action Get Tasks was executed");
            return  _taskLogicService.Get();
        }

        [HttpPost("[action]")]
        public bool AddTask(TaskViewModel taskViewModel)
        {
            logger.LogCreate(this.ToString(), "TaskController Action AddTask was executed");
            return _taskLogicService.Add(taskViewModel);
        }

        [HttpPost("[action]")]
        public bool EditTask(string id, TaskViewModel taskViewModel)
        {
            logger.LogCreate(this.ToString(), "TaskController Action EditTask was executed");
            return _taskLogicService.Edit(id, taskViewModel);
        }
        [HttpPost]
        public bool SetFinished(string id)
        {
            logger.LogCreate(this.ToString(), string.Format("TaskController Action SetFinished was executed for id-{0}",id));
            TaskViewModel task = _taskLogicService.Get(id);

            task.IsFinished = true;

           return _taskLogicService.Edit(id,task);        
        }

        [HttpPost("[action]")]
        public bool DeleteTask(string id, TaskViewModel taskViewModel)
        {
            logger.LogCreate(this.ToString(), "TaskController Action DeleteTask was executed");
            return _taskLogicService.Delete(id, taskViewModel);
        }

        [HttpGet("[action]")]
        public TaskViewModel GetTask(string id)
        {
            logger.LogCreate(this.ToString(), "TaskController Action GetTask was executed");
            return _taskLogicService.Get(id);
        }
    }
}
