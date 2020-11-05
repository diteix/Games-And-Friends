using GamesAndFriends.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamesAndFriends.Data.Context
{
    public class GameDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        { }
    }
}