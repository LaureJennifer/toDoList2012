using TodoList.Models;
using TodoList.Models.Pagination;
using Task = todoListApi.Entities.Task;
namespace todoListApi.Repositories
{
    public interface ITaskRepository
    {
        public Task<Task> Create(Task task);
        public Task<Task> Update(Task task);
        public Task<Task> Delete(Task task);
        public Task<Task> GetById(Guid id);
        public Task<PagedList<Task>> GetByName(TaskListSearch taskListSearch);
    }
}
