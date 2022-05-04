//using Geico.Controllers;
//using T = Geico.Models;
//using Geico.Services;
//using System;
//using System.Collections.Generic;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;
//using Microsoft.Extensions.Logging;
////using Xunit.Sdk;
//using Xunit;
//using Microsoft.AspNetCore.Mvc;
////using Moq;
////using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace GeicoUnitTests
//{
//    public class TasksControllerTests
//    {
//        private readonly ITasksService _service;
//        private readonly TaskController _controller;

//        public TasksControllerTests()
//        {
//            using var logFactory = LoggerFactory.Create(builder => builder.AddConsole());
//            var taskServiceLogger = logFactory.CreateLogger<MockTaskService>();
//            var taskControllerLogger = logFactory.CreateLogger<TaskController>();

//            _service = new MockTaskService(taskServiceLogger);
//            _controller = new TaskController(taskControllerLogger, _service);
//        }

//        [Fact]
//        public void ReturnsOk()
//        {
//            //var taskService = new Mock<ITasksService>();
//            //taskService.Setup(x => x.GetTasks()).Returns(GetTempTasks);

//            var okResult = _controller.Get();
//            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
//        }

//        [Fact]
//        public void ReturnsList()
//        {
//            var okResult = _controller.Get();
//            OkObjectResult result = (OkObjectResult)okResult;
//            var resultList = result.Value as IList<T.Task>;
//            Assert.True(resultList.Count > 0);
//        }

//        [Fact]
//        public void CreateTask()
//        {
//            DateTime dueDate = DateTime.Now.AddDays(1);
//            T.Task task = new T.Task(0, "CreateTask", "CreateDescription", dueDate, new DateTime(), new DateTime(), T.Priority.Medium, T.Status.New);
//            var okResult = _controller.Create(task);
//            OkObjectResult result = (OkObjectResult)okResult;

//            Assert.IsType<OkObjectResult>(result);
//            Assert.IsType<T.Task>(result.Value as T.Task);

//            T.Task taskResult = result.Value as T.Task;
//            Assert.True(taskResult.Id > 0);
//        }


//        [Fact]
//        public void UpdateTask()
//        {
//            T.Task task = new T.Task(1, "CreateTask", "CreateDescription", new DateTime(), new DateTime(), new DateTime(), T.Priority.Low, T.Status.Finished);
//            var okResult = _controller.Update(task);
//            OkObjectResult result = (OkObjectResult)okResult;

//            Assert.IsType<OkObjectResult>(result);
//            Assert.IsType<T.Task>(result.Value as T.Task);

//            T.Task taskResult = result.Value as T.Task;
//            Assert.True(taskResult.Id == task.Id);
//            Assert.True(taskResult.Name == task.Name);
//            Assert.True(taskResult.Priority == task.Priority);
//            Assert.True(taskResult.Status == task.Status);
//        }

//        [Fact]
//        public void CreateTask_Too_Many_High_Priority()
//        {
//            T.Task task = new T.Task(0, "CreateTask", "CreateDescription", new DateTime(), new DateTime(), new DateTime(), T.Priority.High, T.Status.New);
//            var errorResult = _controller.Create(task);
//            ObjectResult result = (ObjectResult)errorResult;
//            Assert.True(result.StatusCode == 400);
//            Assert.True(result.Value.ToString() == "Too many high priority tasks for the same due date!");
//        }

//        [Fact]
//        public void UpdateTask_Too_Many_High_Priority()
//        {
//            //id 101 is low priority. Let's update it to high
//            //99 other tasks are High and are not finished
//            T.Task task = new T.Task(101, "UpdateTask", "UpdateDescription", new DateTime(), new DateTime(), new DateTime(), T.Priority.High, T.Status.New);
//            var errorResult = _controller.Create(task);
//            ObjectResult result = (ObjectResult)errorResult;
//            Assert.True(result.StatusCode == 400);
//            Assert.True(result.Value.ToString() == "Too many high priority tasks for the same due date!");
//        }

//        public void CreateTask_Due_Date_In_Past()
//        {
//            T.Task task = new T.Task(0, "Crea    teTask", "CreateDescription", new DateTime(), new DateTime(), new DateTime(), T.Priority.High, T.Status.New);
//            var errorResult = _controller.Create(task);
//            ObjectResult result = (ObjectResult)errorResult;
//            Assert.True(result.StatusCode == 400);
//            Assert.True(result.Value.ToString() == "Too many high priority tasks for the same due date!");
//        }

//        [Fact]
//        public void UpdateTask_Id_Does_Not_Exist()
//        {
//            T.Task task = new T.Task(200, "UpdateTask", "UpdateDescription", new DateTime(), new DateTime(), new DateTime(), T.Priority.High, T.Status.Finished);
//            var errorResult = _controller.Update(task);
//            StatusCodeResult result = (StatusCodeResult)errorResult;
//            Assert.True(result.StatusCode == 400);
//        }

//        private IList<T.Task> GetTempTasks()
//        {

//            IList < T.Task > _tasks = new List<T.Task>();

//            for (int i = 0; i < 99; i++)
//            {
//                int taskId = i + 1;
//                var task = new T.Task(taskId, "task" + taskId.ToString(), "Description" + taskId.ToString(), new DateTime(), new DateTime(), new DateTime(), T.Priority.High, T.Status.New);
//                _tasks.Add(task);
//            }
//            //add one more which is not high priority for negative testing
//            var taskLast = new T.Task(101, "task101", "Description101", new DateTime(), new DateTime(), new DateTime(), T.Priority.Low, T.Status.New);
//            _tasks.Add(taskLast);
//            return _tasks;
//        }
//    }
//}
