using GamesAndFriends.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamesAndFriends.Data.Context
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        { }
    }
}