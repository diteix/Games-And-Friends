using MediatR;

namespace GamesAndFriends.Domain.Services.Games.Command 
{
    public class DeleteGameCommand : IRequest
    {
        public int Id { get; set; }
    }
}