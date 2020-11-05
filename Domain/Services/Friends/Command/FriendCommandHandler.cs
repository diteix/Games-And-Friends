using MediatR;
using GamesAndFriends.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using GamesAndFriends.Domain.Repository;

namespace GamesAndFriends.Domain.Services.Friends.Command 
{
    public class FriendCommandHandler : IRequestHandler<AddFriendCommand, Friend>, IRequestHandler<DeleteFriendCommand>, IRequestHandler<UpdateFriendCommand, Friend>
    {
        private readonly IFriendRepository _repository;

        public FriendCommandHandler(IFriendRepository repository)
        {
            this._repository = repository;
        }

        public async Task<Friend> Handle(AddFriendCommand request, CancellationToken cancellationToken)
        {
            return await this._repository.AddAsync(request.Friend);
        }

        public async Task<Unit> Handle(DeleteFriendCommand request, CancellationToken cancellationToken)
        {
            await this._repository.DeleteAsync(request.Id);

            return Unit.Value;
        }

        public async Task<Friend> Handle(UpdateFriendCommand request, CancellationToken cancellationToken)
        {
            return await this._repository.UpdateAsync(request.Id, request.Friend);
        }
    }
}