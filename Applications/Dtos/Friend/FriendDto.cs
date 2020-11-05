using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GamesAndFriends.Application.Dtos.Game;

namespace GamesAndFriends.Application.Dtos.Friend 
{

    public class FriendDto 
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public IList<GameDto> Games { get; set; }
    }
}