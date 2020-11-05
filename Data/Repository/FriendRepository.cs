using System.Collections.Generic;
using System.Threading.Tasks;
using GamesAndFriends.Data.Context;
using GamesAndFriends.Domain.Entities;
using GamesAndFriends.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace GamesAndFriends.Data.Repository
{
    public class FriendRepository : IFriendRepository
    {
        private readonly FriendDbContext _context;

        public FriendRepository(FriendDbContext context)
        {
            this._context = context;
        }

        public async Task<Friend> AddAsync(Friend entity)
        {
            var newFriend = await this._context.Friends.AddAsync(entity);
            await this._context.SaveChangesAsync();

            return newFriend.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var friend = await this._context.Friends.FindAsync(id);
            
            if (friend == null)
            {
                return;
            }

            this._context.Friends.Remove(friend);
            await this._context.SaveChangesAsync();
        }

        public async Task<Friend> GetAsync(int id)
        {
            return await this._context.Friends.Include(s => s.Games).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IList<Friend>> GetAllAsync()
        {
            return await this._context.Friends.Include(s => s.Games).ToListAsync();
        }

        public async Task<Friend> UpdateAsync(int id, Friend entity)
        {
            entity.Id = id;
            
            this._context.Friends.Update(entity);
            await this._context.SaveChangesAsync();

            return entity;
        }
    }
}