using System.Collections.Generic;
using MediatR;
using GamesAndFriends.Domain.Entities;

namespace GamesAndFriends.Domain.Services.Users.Query 
{
    public class GetAllUsersQuery : IRequest<IList<User>> 
    {
        
    }
}