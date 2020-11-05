using AutoMapper;
using GamesAndFriends.Application.Dtos.Friend;
using GamesAndFriends.Application.Dtos.Game;
using GamesAndFriends.Application.Dtos.User;
using GamesAndFriends.Domain.Entities;

namespace GamesAndFriends.Application.Mapper
{
    public class DtoAndEntityMappingProfile : Profile
    {
        public DtoAndEntityMappingProfile()
        {
            CreateMap<GameDto, Game>().ForAllMembers(
            opt => opt.Condition((src, dest, sourceMember) => sourceMember != null));
            CreateMap<Game, GameDto>();
            CreateMap<FriendDto, Friend>().ReverseMap();
            CreateMap<AuthenticateDto, User>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}