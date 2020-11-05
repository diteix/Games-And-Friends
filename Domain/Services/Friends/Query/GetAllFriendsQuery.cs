using System.Collections.Generic;
using MediatR;
using GamesAndFriends.Domain.Entities;

namespace GamesAndFriends.Domain.Services.Friends.Query 
{
    public class GetAllFriendsQuery : IRequest<IList<Friend>> 
    {
        
    }
}