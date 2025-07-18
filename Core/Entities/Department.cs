using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("Departments")]
    public class Department : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        public virtual ICollection<Escalation> Escalations { get; set; }
    }
}