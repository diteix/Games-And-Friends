using System.Collections.Generic;
using System.Threading.Tasks;
using GamesAndFriends.Data.Context;
using GamesAndFriends.Domain.Entities;
using GamesAndFriends.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace GamesAndFriends.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            this._context = context;

            if (this._context.Users.Find(1) != null) 
            {
                return;
            }

            this._context.Users.Add(new User() {
                Id = 1,
                Username = "diego",
                Password = "123"
            });

            this._context.SaveChanges();
        }
        
        public async Task<User> GetAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }

            return await this._context.Users.SingleOrDefaultAsync(s => s.Username == username);
        }

        public async Task<IList<User>> GetAllAsync() 
        {
            return await this._context.Users.ToListAsync();
        }
    }
}