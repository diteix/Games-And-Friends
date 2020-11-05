using System.Collections.Generic;
using MediatR;
using GamesAndFriends.Domain.Entities;

namespace GamesAndFriends.Domain.Services.Games.Query 
{
    public class GetAllGamesQuery : IRequest<IList<Game>> 
    {
        
    }
}