using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GamesAndFriends.Application.Dtos.Friend;
using GamesAndFriends.Application.Services.Interfaces;
using GamesAndFriends.Domain.Entities;
using GamesAndFriends.Domain.Services.Friends.Command;
using GamesAndFriends.Domain.Services.Friends.Query;
using MediatR;

namespace GamesAndFriends.Application.Services.Friends 
{
    public class FriendsApplication : IFriendApplication
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public FriendsApplication(IMediator mediator, IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }

        public async Task<FriendDto> AddAsync(FriendDto entity)
        {
            var command = new AddFriendCommand()
            {
                Friend = this._mapper.Map<Friend>(entity)
            };

            return this._mapper.Map<FriendDto>(await this._mediator.Send(command));
        }

        public async Task DeleteAsync(int id)
        {
            var command = new DeleteFriendCommand()
            {
                Id = id
            };

            await this._mediator.Send(command);
        }

        public async Task<IList<FriendDto>> GetAllAsync()
        {
            return this._mapper.Map<IList<FriendDto>>(await this._mediator.Send(new GetAllFriendsQuery()));
        }

        public async Task<FriendDto> GetAsync(int id)
        {
            var query = new GetFriendQuery()
            {
                Id = id
            };

            return this._mapper.Map<FriendDto>(await this._mediator.Send(query));
        }

        public async Task<FriendDto> UpdateAsync(int id, FriendDto entity)
        {
            var command = new UpdateFriendCommand()
            {
                Id = id,
                Friend = this._mapper.Map<Friend>(entity)
            };

            return this._mapper.Map<FriendDto>(await this._mediator.Send(command));
        }
    }
}