using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("DeliveryLinks")]
    public class DeliveryLink : BaseEntity
    {
        [Required]
        [StringLength(500)]
        public string LongUrl { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public virtual ICollection<SurveyDelivery> SurveyDeliveries { get; set; }
    }
}