using System;
using Microsoft.EntityFrameworkCore;

namespace RpsServer.Models
{
    public class GameContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        // public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }

        public GameContext(DbContextOptions<GameContext> options) : base(options)
        {
        }
    }
}
