using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("Permissions")]
    public class Permission : BaseEntity
    {
        [ForeignKey("Module")]
        public int? ModuleId { get; set; }

        public virtual Module Module { get; set; }

        [ForeignKey("Survey")]
        public int? SurveyId { get; set; }

        public virtual Survey Survey { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
