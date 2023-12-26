using todoList.Models.Enums;

namespace TodoList.Models
{
    public class TaskCreateRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public Priority Priority { get; set; }

        public Status Status { get; set; }
    }
}
