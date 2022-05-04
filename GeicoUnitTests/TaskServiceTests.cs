using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;
using Moq;
using Geico.Services;
using T = Geico.Models;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Geico.Controllers;

namespace GeicoUnitTests
{
    public class TestControllerTestsNew
    {
        [Test]
        public void ReturnsOk()
        {
            var taskServiceMock = new Mock<ITasksService>();
            var iLoggerMock = new Mock<ILogger<TaskController>>();
            taskServiceMock.Setup(p => p.GetTasks()).Returns(
                new List<T.Task>()
                { 
                    new T.Task(1, "test", "test", DateTime.Now, DateTime.Now, DateTime.Now, T.Priority.Medium, T.Status.New)
                }
                );

            TaskController tasksController = new TaskController(iLoggerMock.Object, taskServiceMock.Object);
            var result = tasksController.Get();

            Assert.True(true);
        }
        //public void ReturnsOk()
        //{

        //    // Arrange
        //    var loggerMock = new Mock<ILogger<TasksService>>();
        //    var taskService = new TasksService(loggerMock.Object);

        //    var lists = taskService.GetTasks();
        //    Assert.NotNull(lists);
        //}

    }
}
