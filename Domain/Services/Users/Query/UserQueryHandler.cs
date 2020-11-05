using System.Collections.Generic;
using MediatR;
using GamesAndFriends.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using GamesAndFriends.Domain.Repository;

namespace GamesAndFriends.Domain.Services.Users.Query 
{
    public class UserQueryHandle : IRequestHandler<GetAllUsersQuery, IList<User>>, IRequestHandler<GetUserQuery, User>
    {
        private readonly IUserRepository _repository;

        public UserQueryHandle(IUserRepository repository)
        {
            this._repository = repository;
        }

        public async Task<IList<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await this._repository.GetAllAsync();
        }

        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await this._repository.GetAsync(request.Username);
        }
    }
}