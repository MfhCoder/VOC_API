using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("Tags")]
    public class Tag : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<FeedbackTag> FeedbackTags { get; set; }
    }
}