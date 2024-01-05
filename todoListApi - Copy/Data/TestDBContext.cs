using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using todoListApi.Entities;

namespace todoListApi.Data
{
    public class TestDBContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public TestDBContext(DbContextOptions<TestDBContext> dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<Entities.Task> Tasks { get; set; }
    }
}
