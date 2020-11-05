using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GamesAndFriends.Application.Dtos.User;
using GamesAndFriends.Application.Services.Interfaces;
using GamesAndFriends.Domain.Services.Users.Query;
using MediatR;

namespace GamesAndFriends.Application.Services.Users
{
    public class UserApplication : IUserApplication
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserApplication(IMediator mediator, IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }

        public async Task<IList<AuthenticateDto>> GetAllAsync()
        {
            return this._mapper.Map<IList<AuthenticateDto>>(await this._mediator.Send(new GetAllUsersQuery()));
        }

        public async Task<bool> AuthenticateAsync(AuthenticateDto user)
        {
            var query = new GetUserQuery()
            {
                Username = user.Username
            };

            var storedUser = await this._mediator.Send(query);

            return storedUser.IsValidPassword(user.Password);
        }

        public async Task<UserDto> GetAsync(string username) 
        {
            var query = new GetUserQuery()
            {
                Username = username
            };

            return this._mapper.Map<UserDto>(await this._mediator.Send(query));
        }
    }
}