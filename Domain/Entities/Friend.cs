using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesAndFriends.Domain.Entities
{
    [Table("Friends")]
    public class Friend
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey(nameof(Game.FriendId))]
        public virtual ICollection<Game> Games { get; set; }
    }
}