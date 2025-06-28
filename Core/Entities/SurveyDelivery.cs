using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class SurveyDelivery : BaseEntity
{
    public int BatchId { get; set; }
    public SurveyBatch Batch { get; set; }
    public int MerchantId { get; set; }
    public Merchant? Merchant { get; set; }
    public string Status { get; set; }
    public DateTime? DeliveryTime { get; set; }
    public int RetryCount { get; set; }
    public int LinkId { get; set; }
    public DeliveryLink Link { get; set; }
    public string EncryptionToken { get; set; }
}
