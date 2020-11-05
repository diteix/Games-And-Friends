using MediatR;

namespace GamesAndFriends.Domain.Services.Games.Command 
{
    public class LendGameCommand : IRequest
    {
        public int Id { get; set; }
        public int IdFriend { get; set; }
    }
}