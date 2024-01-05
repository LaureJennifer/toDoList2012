
using Microsoft.EntityFrameworkCore;
using TodoList.Models;
using TodoList.Models.Pagination;
using todoListApi.Data;

namespace todoListApi.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TestDBContext _dbContext;
        public TaskRepository(TestDBContext testDBContext)
        {
            _dbContext = testDBContext;
        }

        public async Task<Entities.Task> Create(Entities.Task task)
        {
            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        public async Task<Entities.Task> Delete(Entities.Task task)
        {
            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        //public async Task<IEnumerable<Entities.Task>> GetTasks()
        //{
        //    return await _dbContext.Tasks.Include(x => x.Assignee).ToListAsync();
        //}

        public async Task<Entities.Task> GetById(Guid id)
        {
            return await _dbContext.Tasks.FindAsync(id);
        }

        public async Task<Entities.Task> Update(Entities.Task task)
        {

            _dbContext.Tasks.Update(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        public async Task<PagedList<Entities.Task>> GetByName(TaskListSearch taskListSearch)
        {
            var query = _dbContext.Tasks.Include(x => x.Assignee).AsQueryable();

            if (!string.IsNullOrEmpty(taskListSearch.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(taskListSearch.Name.ToLower()));
            }
            var count = await query.CountAsync();
            var data =  await query.OrderByDescending(x => x.CreatedDate)
                .Skip((taskListSearch.PageNumber - 1)*taskListSearch.PageSize)
                .Take(taskListSearch.PageSize).ToListAsync();
            return new PagedList<Entities.Task>(data,count,taskListSearch.PageNumber,taskListSearch.PageSize);
        }
    }
}