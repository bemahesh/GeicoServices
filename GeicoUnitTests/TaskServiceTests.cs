using System;
using System.Collections.Generic;
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

        [Test]
        public void Update_TooMany_High_Priority_Tasks()
        {

            var tasks = AddTasks();
            var taskToUpdate = new T.Task(101, "test", "test", DateTime.Now, DateTime.Now, DateTime.Now, T.Priority.High, T.Status.New);

            taskServiceMock.SetupGet(p => p.Tasks).Returns(tasks);
            taskServiceMock.Setup(p => p.UpdateTask(taskToUpdate));

            var result = tasksController.Update(taskToUpdate);
            StatusCodeResult resultOk = (StatusCodeResult)result;

            Assert.True(resultOk.StatusCode == 400);
        }

        [Test]
        public void Create_TooMany_High_Priority_Tasks()
        {
            var tasks = AddTasks();
            var taskToCreate = new T.Task(101, "test", "test", DateTime.Now, DateTime.Now, DateTime.Now, T.Priority.High, T.Status.New);

            taskServiceMock.SetupGet(p => p.Tasks).Returns(tasks);
            taskServiceMock.Setup(p => p.CreateTask(taskToCreate));

            var result = tasksController.Create(taskToCreate);
            StatusCodeResult resultOk = (StatusCodeResult)result;

            Assert.True(resultOk.StatusCode == 400);
        }

        private List<T.Task> AddTasks()
        {
            var tasks = new List<T.Task>();
            DateTime dueDate = DateTime.Now.AddDays(-1);
            for (int i = 0; i < 99; i++)
            {
                int taskId = i + 1;
                var task = new T.Task(taskId, "task" + taskId.ToString(), "Description" + taskId.ToString(), dueDate, new DateTime(), new DateTime(), T.Priority.High, T.Status.New);
                tasks.Add(task);
            }
            //add one more which is not high priority for negative testing
            var taskLast = new T.Task(101, "task101", "Description101", dueDate, new DateTime(), new DateTime(), T.Priority.Low, T.Status.New);
            tasks.Add(taskLast);
            return tasks;
        }

    }
}
