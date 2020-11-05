using System.Threading.Tasks;
using GamesAndFriends.Domain.Entities;

namespace GamesAndFriends.Domain.Repository
{
    public interface IGameRepository : ICrudRepository<Game> 
    { 
        Task LendAsync(int id, int idFriend);
        Task TakeBackAsync(int id);
    }
}