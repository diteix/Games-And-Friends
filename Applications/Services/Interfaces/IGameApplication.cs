using System.Threading.Tasks;
using GamesAndFriends.Application.Dtos.Game;

namespace GamesAndFriends.Application.Services.Interfaces 
{
    public interface IGameApplication : ICrudApplication<GameDto>
    { 
        Task<bool> LendAsync(int id, int idFriend);
        Task TakeBackAsync(int id);
    }
}