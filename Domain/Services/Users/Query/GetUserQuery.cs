using MediatR;
using GamesAndFriends.Domain.Entities;

namespace GamesAndFriends.Domain.Services.Users.Query 
{
    public class GetUserQuery : IRequest<User> 
    {
        public string Username { get; set; }
    }
}