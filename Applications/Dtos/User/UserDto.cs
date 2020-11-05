using System.ComponentModel.DataAnnotations;

namespace GamesAndFriends.Application.Dtos.User 
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
    }
}