using MediatR;
using GamesAndFriends.Domain.Entities;

namespace GamesAndFriends.Domain.Services.Friends.Query 
{
    public class GetFriendQuery : IRequest<Friend> 
    {
        public int Id { get; set; }
    }
}