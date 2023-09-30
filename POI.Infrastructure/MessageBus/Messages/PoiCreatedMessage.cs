namespace POI.Infrastructure.MessageBus.Messages;

public class PoiCreatedMessage
{
    public int PoiId { get; set; }
    public DateTime CreatedAt { get; set; }
}