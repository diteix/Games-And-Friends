using System.Collections.Generic;
using System.Threading.Tasks;
using GamesAndFriends.Data.Context;
using GamesAndFriends.Domain.Entities;
using GamesAndFriends.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace GamesAndFriends.Data.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly GameDbContext _context;

        public GameRepository(GameDbContext context)
        {
            this._context = context;
        }

        public async Task<Game> AddAsync(Game entity)
        {
            var newGame = await this._context.Games.AddAsync(entity);
            await this._context.SaveChangesAsync();

            return newGame.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var game = await this._context.Games.FindAsync(id);
            
            if (game == null)
            {
                await Task.FromException(new KeyNotFoundException());
                return;
            }

            this._context.Games.Remove(game);
            await this._context.SaveChangesAsync();
        }

        public async Task<Game> GetAsync(int id)
        {
            return await this._context.Games.Include(s => s.Friend).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IList<Game>> GetAllAsync()
        {
            return await this._context.Games.Include(s => s.Friend).ToListAsync();
        }

        public async Task<Game> UpdateAsync(int id, Game entity)
        {
            entity.Id = id;

            this._context.Games.Update(entity);
            await this._context.SaveChangesAsync();

            return entity;
        }

        public async Task LendAsync(int id, int idFriend)
        {
            var game = await this._context.Games.FindAsync(id);

            game.FriendId = idFriend;

            this._context.Games.Update(game);
            await this._context.SaveChangesAsync();
        }

        public async Task TakeBackAsync(int id)
        {
            var game = await this._context.Games.FindAsync(id);

            game.FriendId = null;

            this._context.Games.Update(game);
            await this._context.SaveChangesAsync();
        }
    }
}