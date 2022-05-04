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
using Microsoft.AspNetCore.Mvc;

namespace GeicoUnitTests
{
    public class TestControllerTestsNew
    {
        Mock<ITasksService> taskServiceMock;
        Mock<ILogger<TaskController>> iLoggerMock;
        TaskController tasksController;
        public TestControllerTestsNew()
        {
            taskServiceMock = new Mock<ITasksService>();
            iLoggerMock = new Mock<ILogger<TaskController>>();
            tasksController = new TaskController(iLoggerMock.Object, taskServiceMock.Object);
        }
        [Test]
        public void ReturnsOk()
        {
            taskServiceMock.Setup(p => p.GetTasks())
                .Returns(new List<T.Task>(){ new T.Task(1, "test", "test", DateTime.Now, DateTime.Now, DateTime.Now, T.Priority.Medium, T.Status.New)});

            var result = tasksController.Get();
            OkObjectResult resultOk = (OkObjectResult)result;

            Assert.True(resultOk.StatusCode == 200);
        }

        [Test]
        public void CreateNewTask()
        {
            var taskToCreate = new T.Task(0, "test", "test", DateTime.Now, DateTime.Now, DateTime.Now, T.Priority.Medium, T.Status.New);
            var taskToReturn = new T.Task(11, "test", "test", DateTime.Now, DateTime.Now, DateTime.Now, T.Priority.Medium, T.Status.New);

            taskServiceMock.Setup(p => p.CreateTask(taskToCreate)).Returns(taskToReturn);

            var result = tasksController.Create(taskToCreate);
            OkObjectResult resultOk = (OkObjectResult)result;

            Assert.True(resultOk.StatusCode == 200);

            var returnedTask = (T.Task) resultOk.Value;

            Assert.True(returnedTask.Id == taskToReturn.Id);
        }

        [Test]
        public void CreateNewTask_PastDueDate()
        {
            DateTime dueDate = DateTime.Now.AddDays(-1);
            var taskToCreate = new T.Task(0, "test", "test", dueDate, DateTime.Now, DateTime.Now, T.Priority.Medium, T.Status.New);
            taskServiceMock.Setup(p => p.CreateTask(taskToCreate));

            var result = tasksController.Create(taskToCreate);
            StatusCodeResult resultOk = (StatusCodeResult)result;

            Assert.True(resultOk.StatusCode == 400);
        }

        [Test]
        public void UpdateTask()
        {
            DateTime dueDate = DateTime.Now.AddDays(1);
            var taskToUpdate = new T.Task(11, "test", "test", dueDate, DateTime.Now, DateTime.Now, T.Priority.Medium, T.Status.New);
            var taskToReturn = new T.Task(11, "test", "test", dueDate, DateTime.Now, DateTime.Now, T.Priority.Medium, T.Status.New);

            taskServiceMock.Setup(p => p.UpdateTask(taskToUpdate)).Returns(taskToReturn);

            var result = tasksController.Update(taskToUpdate);
            OkObjectResult resultOk = (OkObjectResult)result;
            Assert.True(resultOk.StatusCode == 200);

            var returnedTask = (T.Task)resultOk.Value;
            Assert.True(returnedTask.Id == taskToReturn.Id);
        }

        [Test]
        public void Update_Non_Existing_Task()
        {
            var taskToUpdate = new T.Task(0, "test", "test", DateTime.Now, DateTime.Now, DateTime.Now, T.Priority.Medium, T.Status.New);
            taskServiceMock.Setup(p => p.UpdateTask(taskToUpdate));

            var result = tasksController.Update(taskToUpdate);
            StatusCodeResult resultOk = (StatusCodeResult)result;

            Assert.True(resultOk.StatusCode == 400);
        }

    }
}
