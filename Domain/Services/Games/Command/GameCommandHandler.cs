using MediatR;
using GamesAndFriends.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using GamesAndFriends.Domain.Repository;

namespace GamesAndFriends.Domain.Services.Games.Command 
{
    public class GameCommandHandler 
    : IRequestHandler<AddGameCommand, Game>, IRequestHandler<DeleteGameCommand>, IRequestHandler<UpdateGameCommand, Game>,
    IRequestHandler<LendGameCommand>, IRequestHandler<TakeBackGameCommand>
    {
        private readonly IGameRepository _repository;

        public GameCommandHandler(IGameRepository repository)
        {
            this._repository = repository;
        }

        public async Task<Game> Handle(AddGameCommand request, CancellationToken cancellationToken)
        {
            return await this._repository.AddAsync(request.Game);
        }

        public async Task<Unit> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            await this._repository.DeleteAsync(request.Id);

            return Unit.Value;
        }

        public async Task<Game> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
        {
            return await this._repository.UpdateAsync(request.Id, request.Game);
        }

        public async Task<Unit> Handle(LendGameCommand request, CancellationToken cancellationToken)
        {
            await this._repository.LendAsync(request.Id, request.IdFriend);

            return Unit.Value;
        }

        public async Task<Unit> Handle(TakeBackGameCommand request, CancellationToken cancellationToken)
        {
            await this._repository.TakeBackAsync(request.Id);

            return Unit.Value;
        }
    }
}