using T = Geico.Models;

namespace Geico.Services
{
    public interface ITasksService
    {
        public T.Task CreateTask(T.Task task);
        public T.Task UpdateTask(T.Task task);
        public IList<T.Task> GetTasks();
    }
}
