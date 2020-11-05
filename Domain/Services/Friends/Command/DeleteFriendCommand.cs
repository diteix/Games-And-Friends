using MediatR;

namespace GamesAndFriends.Domain.Services.Friends.Command 
{
    public class DeleteFriendCommand : IRequest
    {
        public int Id { get; set; }
    }
}