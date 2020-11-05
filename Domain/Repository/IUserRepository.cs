using System.Collections.Generic;
using System.Threading.Tasks;
using GamesAndFriends.Domain.Entities;

namespace GamesAndFriends.Domain.Repository
{
    public interface IUserRepository
    {
        Task<User> GetAsync(string username);
        Task<IList<User>> GetAllAsync();
    }
}