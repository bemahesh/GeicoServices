namespace Geico.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Priority Priority { get; set; }
        public Status Status { get; set; }

        public Task()
        {
        }
        public Task(int id, string name, string description, DateTime dueDate, DateTime startDate, DateTime endDate, Priority priority, Status status)
        {
            Id = id;
            Name = name;
            Description = description;
            DueDate = dueDate;
            StartDate = startDate;
            EndDate = endDate;
            Priority = priority;
            Status = status;
        }
    }
}
