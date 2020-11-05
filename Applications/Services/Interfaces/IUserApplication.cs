using System.Collections.Generic;
using System.Threading.Tasks;
using GamesAndFriends.Application.Dtos.User;

namespace GamesAndFriends.Application.Services.Interfaces 
{
    public interface IUserApplication 
    {
        Task<bool> AuthenticateAsync(AuthenticateDto user);
        Task<IList<AuthenticateDto>> GetAllAsync();
        Task<UserDto> GetAsync(string username);
    }
}