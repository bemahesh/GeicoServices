using T = Geico.Models;

namespace Geico.Services
{
    public class TasksService : ITasksService
    {
        private static readonly object _lock = new object();
        private static IList<T.Task> _tasks;
        private readonly ILogger<TasksService> _logger;

        public IList<T.Task> Tasks
        {
            get { return _tasks; }
        }
        public TasksService(ILogger<TasksService> logger)
        {
            _logger = logger;
            _tasks = GetTempTasks();
        }

        public Models.Task CreateTask(Models.Task newTask)
        {
            try
            {
                ValidateData(newTask);
                int id = _tasks.Max(task => task.Id);

                newTask.Id = id + 1;
                _tasks.Add(newTask);
                return newTask;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString(), newTask);
                throw ex;
            }
        }

        public Models.Task UpdateTask(Models.Task updateTask)
        {
            try
            {
                //TODO:should update also consider "When creating a Task, the Due Date cannot be in the past"?
                //Once task has been created, do we need to support updating the due date to past (less than now)? probably not.
                //Possible discussion point

                int totalCount = GetHighPriorityNotFinishedTotal(updateTask);
                //TODO: Create config item for 100
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
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString(), updateTask);
                throw ex;
            }
        }

        public IList<T.Task> GetTasks()
        {
            try
            {
                //TODO: THERE SHOULD BE PAGING WITH DEFAULT PAGE SIZE
                return _tasks;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
                return null;
            }
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
            //TODO: SHOULD BE CALLING SQL AND READING/UPDATING THE DESTINATION
            if (_tasks != null)
            {
                return _tasks;
            }
            lock (_lock)
            {
                if (_tasks == null)
                {
                    _tasks = new List<T.Task>();

                    DateTime dueDate = DateTime.Now.AddDays(-1);
                    for (int i = 0; i < 99; i++)
                    {
                        int taskId = i + 1;
                        var task = new T.Task(taskId, "task" + taskId.ToString(), "Description" + taskId.ToString(), dueDate, new DateTime(), new DateTime(), T.Priority.High, T.Status.New);
                        _tasks.Add(task);
                    }

                    //add one more which is not high priority for negative testing
                    var taskLast = new T.Task(101, "task101", "Description101", dueDate, new DateTime(), new DateTime(), T.Priority.Low, T.Status.New);
                    _tasks.Add(taskLast);
                }
            }
            
            return _tasks;
        }
    }
}
