using GamesAndFriends.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamesAndFriends.Data.Context
{
    public class FriendDbContext : DbContext
    {
        public DbSet<Friend> Friends { get; set; }

        public FriendDbContext(DbContextOptions<FriendDbContext> options) : base(options)
        { }
    }
}