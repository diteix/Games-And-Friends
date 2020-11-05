using MediatR;
using GamesAndFriends.Domain.Entities;

namespace GamesAndFriends.Domain.Services.Games.Command 
{
    public class UpdateGameCommand : IRequest<Game> 
    {
        public int Id { get; set; }
        public Game Game { get; set; }
    }
}