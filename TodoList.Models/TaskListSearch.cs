using TodoList.Models.Pagination;

namespace TodoList.Models
{
    public class TaskListSearch: PagingParameter
    {
        public string? Name { get; set; }


    }
}
