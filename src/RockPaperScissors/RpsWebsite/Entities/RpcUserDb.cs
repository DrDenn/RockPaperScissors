using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RpsWebsite.Entities
{
    /// <summary>
    /// Microsoft Entity Framework handles the creation of our databases.
    /// This class represents a user database containing all the info needed.
    /// </summary>
    public sealed class RpcUserDb : IdentityDbContext<User>
    {
        public RpcUserDb(DbContextOptions options) : base(options)
        {
            // I was missing this line initially.
            // Note to self: don't miss this line.
            Database.EnsureCreated();
        }
    }
}
