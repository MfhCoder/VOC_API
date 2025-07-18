using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("Modules")]
    public class Module : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}