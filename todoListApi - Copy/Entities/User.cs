using Microsoft.AspNetCore.Identity;

namespace todoListApi.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Description { get; set; }
    }
}
