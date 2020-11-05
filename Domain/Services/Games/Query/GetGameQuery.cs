using MediatR;
using GamesAndFriends.Domain.Entities;

namespace GamesAndFriends.Domain.Services.Games.Query 
{
    public class GetGameQuery : IRequest<Game> 
    {
        public int Id { get; set; }
    }
}