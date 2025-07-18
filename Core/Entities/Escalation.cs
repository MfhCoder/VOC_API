using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("Escalations")]
    public class Escalation : BaseEntity
    {
        [Required]
        [ForeignKey("Feedback")]
        public int FeedbackId { get; set; }

        public virtual Feedback Feedback { get; set; }

        [Required]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(1000)]
        public string Comment { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? ResolvedAt { get; set; }
    }
}