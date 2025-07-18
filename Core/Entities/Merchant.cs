using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("Merchants")]
    public class Merchant : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public int? TransactionCount { get; set; }

        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? TransactionValue { get; set; }

        [StringLength(50)]
        public string? Type { get; set; }

        [Range(0, int.MaxValue)]
        public int? TerminalCount { get; set; }

        [StringLength(100)]
        public string? License { get; set; }

        [StringLength(20)]
        public string? PosMcc { get; set; }

        [StringLength(100)]
        public string? Industry { get; set; }

        [StringLength(200)]
        public string? Location { get; set; }

        [StringLength(100)]
        public string? Product { get; set; }

        [StringLength(100)]
        public string? Ledger { get; set; }

        [StringLength(200)]
        public string? MerchantSettlementBank { get; set; }

        [StringLength(10)]
        public string? Tenure { get; set; }

        [Range(0, int.MaxValue)]
        public int? TenureInDays { get; set; }

        public DateTime? LastTransaction { get; set; }
        public DateTime? LastTicket { get; set; }
        public DateTime? LastSurvey { get; set; }
        public DateTime? LastFeedback { get; set; }
        public DateTime? LastEscalation { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Phone]
        [StringLength(20)]
        public string? PhoneNo { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<SurveyDelivery> SurveyDeliveries { get; set; }
    }
}