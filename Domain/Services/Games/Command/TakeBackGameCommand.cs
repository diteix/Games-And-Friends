using MediatR;

namespace GamesAndFriends.Domain.Services.Games.Command 
{
    public class TakeBackGameCommand : IRequest
    {
        public int Id { get; set; }
    }
}