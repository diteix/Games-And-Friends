using System.ComponentModel.DataAnnotations;

namespace GamesAndFriends.Application.Dtos.User 
{
    public class AuthenticateDto 
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}