using MediatR;
using GamesAndFriends.Domain.Entities;

namespace GamesAndFriends.Domain.Services.Friends.Command 
{
    public class UpdateFriendCommand : IRequest<Friend> 
    {
        public int Id { get; set; }
        public Friend Friend { get; set; }
    }
}