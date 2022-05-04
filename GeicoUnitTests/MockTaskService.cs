using Geico.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T = Geico.Models;

namespace GeicoUnitTests
{
    //public class MockTaskService : ITasksService
    //{
    //    private static IList<T.Task> _tasks;
    //    private readonly ILogger<MockTaskService> _logger;
    //    public MockTaskService(ILogger<MockTaskService> logger)
    //    {
    //        _logger = logger;
    //        _tasks = null;
    //    }
    //    public virtual T.Task CreateTask(T.Task task)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public virtual IList<T.Task> GetTasks()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public virtual T.Task UpdateTask(T.Task task)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class MockTaskService : ITasksService
    {
        private static readonly object _lock = new object();
        private static IList<T.Task> _tasks;
        private readonly ILogger<MockTaskService> _logger;
        public MockTaskService(ILogger<MockTaskService> logger)
        {
            _logger = logger;
            _tasks = GetTempTasks();
        }
        public IList<T.Task> GetTasks()
        {
            return _tasks;
        }

        public T.Task CreateTask(T.Task newTask)
        {
            //TODO: CENTRALIZE THE LOGIC BETWEEN SERVICE AND MOCK SERVICE
            // find proper way to mock service
            ValidateData(newTask);
            int id = _tasks.Max(task => task.Id);

            newTask.Id = id + 1;
            _tasks.Add(newTask);

            return newTask;
        }
        public T.Task UpdateTask(T.Task updateTask)
        {
            //TODO: CENTRALIZE THE LOGIC BETWEEN SERVICE AND MOCK SERVICE
            // find proper way to mock service
            int totalCount = GetHighPriorityNotFinishedTotal(updateTask);
            if (totalCount >= 100)
            {
                throw new Exception("Too many high priority tasks for the same due date!");
            }

            var updatedTask = _tasks.SingleOrDefault(p => p.Id == updateTask.Id);
            if (updatedTask != null)
            {
                updatedTask.Description = updateTask.Description;
                updatedTask.Name = updateTask.Name;
                updatedTask.DueDate = updateTask.DueDate;
                updatedTask.StartDate = updateTask.StartDate;
                updatedTask.EndDate = updateTask.EndDate;
                updatedTask.Priority = updateTask.Priority;
                updatedTask.Status = updateTask.Status;
            }
            return updatedTask;
        }

        private void ValidateData(T.Task task)
        {
            //pick up all the tasks which are high priority, not finished which are of same due date excluding current task
            //For the due date: considering month, date and year only and ignoring time
            int totalCount = GetHighPriorityNotFinishedTotal(task);
            if (totalCount >= 100)
            {
                throw new Exception("Too many high priority tasks for the same due date!");
            }
            if (task.DueDate < DateTime.Now)
            {
                throw new Exception("Due date can't be in the past");
            }
        }

        private int GetHighPriorityNotFinishedTotal(T.Task createUpdateTask)
        {
            //get all other tasks (not finished and high priority) other than current one being worked on.
            var highPriorityNotFinished =
                                _tasks.Where(t => t.Priority == T.Priority.High
                                                    && t.Status != T.Status.Finished
                                                    && t.DueDate.ToShortDateString() == createUpdateTask.DueDate.ToShortDateString()
                                                    && t.Id != createUpdateTask.Id).ToList();

            int totalCount = highPriorityNotFinished.Count;

            //now add current one being saved if it has high priorty
            if (createUpdateTask.Priority == T.Priority.High)
            {
                totalCount = totalCount + 1;
            }

            return totalCount;
        }

        private IList<T.Task> GetTempTasks()
        {
            //THIS ACTS LIKE DB CALL.  
            //TODO: SHOULD BE CALLING SQL OR WHATEVER DB AND READING/UPDATING THE DESTINATION
            if (_tasks != null)
            {
                return _tasks;
            }

            lock (_lock)
            {
                if (_tasks == null)
                {
                    _tasks = new List<T.Task>();


                    for (int i = 0; i < 99; i++)
                    {
                        int taskId = i + 1;
                        var task = new T.Task(taskId, "task" + taskId.ToString(), "Description" + taskId.ToString(), new DateTime(), new DateTime(), new DateTime(), T.Priority.High, T.Status.New);
                        _tasks.Add(task);
                    }

                    //add one more which is not high priority for negative testing
                    var taskLast = new T.Task(101, "task101", "Description101", new DateTime(), new DateTime(), new DateTime(), T.Priority.Low, T.Status.New);
                    _tasks.Add(taskLast);
                }
            }
            return _tasks;
        }
    }
}
