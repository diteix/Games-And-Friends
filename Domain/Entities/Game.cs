using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesAndFriends.Domain.Entities
{
    [Table("Games")]
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey(nameof(Friend))]
        public int? FriendId { get; set; }
        public Friend Friend { get; set; }

        public bool IsLent() {
            return this.FriendId.HasValue;
        }
    }
}