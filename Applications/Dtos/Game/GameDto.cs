using System.ComponentModel.DataAnnotations;
using GamesAndFriends.Application.Dtos.Friend;

namespace GamesAndFriends.Application.Dtos.Game 
{

    public class GameDto 
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public FriendDto Friend { get; set; }
    }
}