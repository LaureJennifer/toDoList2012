using TodoList.Models;
using TodoList.Models.Pagination;

namespace toDoList2712.Services
{
    public interface ITaskApiClient
    {
        Task<PagedList<TaskDto>> GetTasks(TaskListSearch taskListSearch);
        Task<TaskDto> GetTaskDetails(string id);

        Task<bool> CreateTask(TaskCreateRequest taskCreateRequest);
        Task<bool> UpdateTask(Guid id, TaskUpdateRequest taskUpdateRequest);

        Task<bool> DeleteTask(Guid id);

        Task<bool> NameTask(Guid id, NameTaskRequest nameTaskRequest);


    }
}
