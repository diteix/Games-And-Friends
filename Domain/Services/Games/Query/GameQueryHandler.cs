using System.Collections.Generic;
using MediatR;
using GamesAndFriends.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using GamesAndFriends.Domain.Repository;

namespace GamesAndFriends.Domain.Services.Games.Query 
{
    public class GameQueryHandler : IRequestHandler<GetAllGamesQuery, IList<Game>>, IRequestHandler<GetGameQuery, Game>
    {
        private readonly IGameRepository _repository;

        public GameQueryHandler(IGameRepository repository)
        {
            this._repository = repository;
        }

        public async Task<IList<Game>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
        {
            return await this._repository.GetAllAsync();
        }

        public async Task<Game> Handle(GetGameQuery request, CancellationToken cancellationToken)
        {
            return await this._repository.GetAsync(request.Id);
        }
    }
}