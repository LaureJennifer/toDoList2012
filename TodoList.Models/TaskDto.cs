using todoList.Models.Enums;

namespace TodoList.Models
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Priority Priority { get; set; }

        public Guid? AssigneeId { get; set; }

        public DateTime CreatedDate { get; set; }

        public Status Status { get; set; }
    }
}

