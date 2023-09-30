namespace POI.Infrastructure.MessageBus.Messages;

public class PoiDeletedMessage
{
    public int PoiId { get; set; }
    public DateTime CreatedAt { get; set; }
}