using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.Models.Pagination;
using todoListApi.Repositories;

namespace todoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetByName([FromQuery] TaskListSearch taskListSearch)
        {
            var pageList = await _taskRepository.GetByName(taskListSearch);

            var taskDtosByName = pageList.Items.Select(x => new TaskDto()
            {
                Status = x.Status,
                Priority = x.Priority,
                Name = x.Name,
                CreatedDate = x.CreatedDate,
                AssigneeId = x.AssigneeId,
                Id = x.Id,
            });
            return Ok(new PagedList<TaskDto>(taskDtosByName.ToList(),
                pageList.MetaData.TotalCount,
                pageList.MetaData.CurrentPage,
                pageList.MetaData.PageSize));
        }
        //public async Task<IActionResult> GetAll()
        //{
        //    var tasks = await _taskRepository.GetTasks();
        //    var taskDtos = tasks.Select(x => new TaskDto()
        //    {
        //        Status = x.Status,
        //        Priority = x.Priority,
        //        Name = x.Name,
        //        CreatedDate = x.CreatedDate,
        //        AssigneeId = x.AssigneeId,
        //        Id = x.Id,
        //    });
        //    return Ok(taskDtos);
        //}
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskCreateRequest taskCreateRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = await _taskRepository.Create(new Entities.Task()
            {
                Name = taskCreateRequest.Name,
                Priority = taskCreateRequest.Priority,
                Status = taskCreateRequest.Status,
                CreatedDate = DateTime.Now,
                Id = taskCreateRequest.Id

            });
            return CreatedAtAction(nameof(GetById), new { taskCreateRequest.Id }, taskCreateRequest);
        }
        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> Update(Guid id, [FromBody] TaskUpdateRequest taskUpdateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var TaskUpdate = await _taskRepository.GetById(id);
            if (TaskUpdate == null)
            {
                return NotFound($"{id} is not found");
            }
            TaskUpdate.Name = taskUpdateRequest.Name;
            TaskUpdate.Priority = taskUpdateRequest.Priority;
            TaskUpdate.Status = taskUpdateRequest.Status;
            TaskUpdate.CreatedDate = DateTime.Now;

            var taskResult = await _taskRepository.Update(TaskUpdate);
            return Ok(new TaskDto()
            {
                Name = taskResult.Name,
                CreatedDate = taskResult.CreatedDate,
                AssigneeId = taskResult.AssigneeId,
                Priority = taskResult.Priority,
                Status = taskResult.Status,
                Id = taskResult.Id
            });
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var task = await _taskRepository.GetById(id);
            if (task == null)
            {
                return NotFound($"{id} is not found");
            }
            return Ok(new TaskDto()
            {
                Name = task.Name,
                CreatedDate = task.CreatedDate,
                AssigneeId = task.AssigneeId,
                Priority = task.Priority,
                Status = task.Status,
                Id = task.Id
            });
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var task = await _taskRepository.GetById(id);
            if (task == null) return NotFound($"{id} is not found");

            await _taskRepository.Delete(task);
            return Ok(new TaskDto()
            {
                Name = task.Name,
                Status = task.Status,
                Id = task.Id,
                AssigneeId = task.AssigneeId,
                Priority = task.Priority,
                CreatedDate = task.CreatedDate
            });
        }


        [HttpPut]
        [Route("{id}/taskname")]
        public async Task<IActionResult> NameTask(Guid id, [FromBody] NameTaskRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskFromDb = await _taskRepository.GetById(id);

            if (taskFromDb == null)
            {
                return NotFound($"{id} is not found");
            }

            taskFromDb.Name = request.Name;
            taskFromDb.CreatedDate = DateTime.Now;

            var taskResult = await _taskRepository.Update(taskFromDb);

            return Ok(new TaskDto()
            {
                Name = taskResult.Name,
                Status = taskResult.Status,
                Id = taskResult.Id,
                AssigneeId = taskResult.AssigneeId,
                Priority = taskResult.Priority,
                CreatedDate = taskResult.CreatedDate
            });
        }

    }
}
