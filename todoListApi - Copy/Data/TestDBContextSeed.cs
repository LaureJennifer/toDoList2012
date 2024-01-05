using Microsoft.AspNetCore.Identity;
using todoListApi.Entities;

namespace todoListApi.Data
{
    public class TestDBContextSeed
    {
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public async System.Threading.Tasks.Task SeedAsync(TestDBContext dbContext, ILogger<TestDBContext> logger)
        {
            if (!dbContext.Users.Any())
            {
                var newUser = new User()
                {
                    Id = Guid.NewGuid(),
                    Description = "Description of User1",
                    PhoneNumber = "0349485127"
                };
                newUser.PasswordHash = _passwordHasher.HashPassword(newUser, "Admin@123");
                dbContext.Users.Add(newUser);
            }

            if (!dbContext.Tasks.Any())
            {
                dbContext.Tasks.Add(new Entities.Task()
                {
                    Id = Guid.NewGuid(),
                    Name = "username1",
                    CreatedDate = DateTime.Now,
                    Priority = 0,
                    Status = todoList.Models.Enums.Status.Success,
                });
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
