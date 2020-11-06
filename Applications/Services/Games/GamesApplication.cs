using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GamesAndFriends.Application.Dtos.Game;
using GamesAndFriends.Application.Services.Interfaces;
using GamesAndFriends.Domain.Entities;
using GamesAndFriends.Domain.Services.Games.Command;
using GamesAndFriends.Domain.Services.Games.Query;
using MediatR;

namespace GamesAndFriends.Application.Services.Games 
{
    public class GamesApplication : IGameApplication
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GamesApplication(IMediator mediator, IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }

        public async Task<GameDto> AddAsync(GameDto entity)
        {
            var command = new AddGameCommand()
            {
                Game = this._mapper.Map<Game>(entity)
            };

            return this._mapper.Map<GameDto>(await this._mediator.Send(command));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var query = new GetGameQuery()
            {
                Id = id
            };

            var game = await this._mediator.Send(query);

            if (game.IsLent()) 
            {
                return false;
            }

            var command = new DeleteGameCommand()
            {
                Id = id
            };

            await this._mediator.Send(command);

            return true;
        }

        public async Task<IList<GameDto>> GetAllAsync()
        {
            return this._mapper.Map<IList<GameDto>>(await this._mediator.Send(new GetAllGamesQuery()));
        }

        public async Task<GameDto> GetAsync(int id)
        {
            var query = new GetGameQuery()
            {
                Id = id
            };

            return this._mapper.Map<GameDto>(await this._mediator.Send(query));
        }

        public async Task<GameDto> UpdateAsync(int id, GameDto entity)
        {
            var command = new UpdateGameCommand()
            {
                Id = id,
                Game = this._mapper.Map<Game>(entity)
            };

            return this._mapper.Map<GameDto>(await this._mediator.Send(command));
        }

        public async Task<bool> LendAsync(int id, int idFriend)
        {
            var query = new GetGameQuery()
            {
                Id = id
            };

            var game = await this._mediator.Send(query);

            if (game.IsLent()) 
            {
                return false;
            }

            var command = new LendGameCommand()
            {
                Id = id,
                IdFriend = idFriend
            };

            await this._mediator.Send(command);

            return true;
        }

        public async Task TakeBackAsync(int id)
        {
            var command = new TakeBackGameCommand()
            {
                Id = id
            };

            await this._mediator.Send(command);
        }
    }
}