using Microsoft.AspNetCore.Identity;

namespace Upwork.Testing.Data.Models.Auth
{
    public class User : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
