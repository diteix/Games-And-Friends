using MediatR;
using GamesAndFriends.Domain.Entities;

namespace GamesAndFriends.Domain.Services.Friends.Command 
{
    public class AddFriendCommand : IRequest<Friend> 
    {
        public Friend Friend { get; set; }
    }
}