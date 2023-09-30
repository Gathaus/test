namespace POI.Infrastructure.MessageBus.Messages;

public class PoiUpdatedMessage
{
    public int PoiId { get; set; }
    public DateTime CreatedAt { get; set; }
}