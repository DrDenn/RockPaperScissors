using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RpsWebsite.Entities
{
    /// <summary>
    /// Asp.NET Core Identity EntityFramework User
    /// All these lovely libraries handle all the auth-n for us.
    /// </summary>
    public class User : IdentityUser
    {
        public User()
        {

        }

        public User(string userName)
        {
            UserName = userName;
        }
    }
}
