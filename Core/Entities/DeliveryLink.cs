namespace Core.Entities;

public class DeliveryLink : BaseEntity
{
    public string LongUrl { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; }
}