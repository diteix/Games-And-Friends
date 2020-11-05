using MediatR;
using GamesAndFriends.Domain.Entities;

namespace GamesAndFriends.Domain.Services.Games.Command 
{
    public class AddGameCommand : IRequest<Game> 
    {
        public Game Game { get; set; }
    }
}