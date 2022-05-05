using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using T = Geico.Models;
using Geico.Services;

namespace Geico.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;

        private readonly ITasksService _service;
        public TaskController(ILogger<TaskController> logger, ITasksService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public IActionResult Create(T.Task task)
        {
            try
            {
                var updated = _service.CreateTask(task);
                if (updated == null)
                {
                    return StatusCode(400);
                }
                return Ok(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), task);
                string customMessage = string.Empty;
                if (ex.Message.Contains("Due date can't be in the past"))
                { 
                    customMessage = ex.Message;
                    return StatusCode(400, customMessage);
                }
                if (ex.Message.Contains("Too many high priority tasks for the same due date!"))
                {
                    customMessage = ex.Message;
                    return StatusCode(400, customMessage);
                }
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        public IActionResult Update(T.Task task)
        {
            try
            {
                var updated =  _service.UpdateTask(task);
                if (updated == null)
                {
                    return StatusCode(400);
                }
                return Ok(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), task);
                string customMessage = string.Empty;
                if (ex.Message.Contains("Due date can't be in the past"))
                {
                    customMessage = ex.Message;
                    return StatusCode(400, customMessage);
                }
                if (ex.Message.Contains("Too many high priority tasks for the same due date!"))
                {
                    customMessage = ex.Message;
                    return StatusCode(400, customMessage);
                }
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var tasks = _service.GetTasks();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

    }
}
