using System.Collections.Generic;
using MediatR;
using GamesAndFriends.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using GamesAndFriends.Domain.Repository;

namespace GamesAndFriends.Domain.Services.Friends.Query 
{
    public class FriendQueryHandle : IRequestHandler<GetAllFriendsQuery, IList<Friend>>, IRequestHandler<GetFriendQuery, Friend>
    {
        private readonly IFriendRepository _repository;

        public FriendQueryHandle(IFriendRepository repository)
        {
            this._repository = repository;
        }

        public async Task<IList<Friend>> Handle(GetAllFriendsQuery request, CancellationToken cancellationToken)
        {
            return await this._repository.GetAllAsync();
        }

        public async Task<Friend> Handle(GetFriendQuery request, CancellationToken cancellationToken)
        {
            return await this._repository.GetAsync(request.Id);
        }
    }
}