using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace RpsWebsite.Entities
{
    /// <summary>
    /// Asp.NET Core Identity EntityFramework User
    /// All these lovely libraries handle all the auth-n for us.
    /// </summary>
    public sealed class User : IdentityUser
    {
        public User()
        {

        }

        /// <summary>
        /// Creates a new User.
        /// Should only happen when registering a new user, otherwise Entity Framework 
        /// will read an existing user from the database.
        /// </summary>
        /// <param name="userName">The name of the user</param>
        public User(string userName)
        {
            UserName = userName;
            UserId = Guid.NewGuid();

            Wins = 0;
            Losses = 0;
            Draws = 0;
        }

        /// <summary>A unique Key for the backend to identify this user.</summary>
        public Guid UserId { get; set; }

        /// <summary>Lifetime number of wins for this user.</summary>
        public int Wins { get; set; }

        /// <summary>Lifetime number of losses for this user.</summary>
        public int Losses { get; set; }

        /// <summary>Lifetime number of losses for this user.</summary>
        public int Draws { get; set; }
    }
}
