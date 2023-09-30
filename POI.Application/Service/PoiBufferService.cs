namespace POI.Application.Service;

public class PoiBufferService
{
    private static List<int> BufferedPoiIds { get; set; } = new List<int>();

    public void AddPoiId(int poi)
    {
        BufferedPoiIds.Add(poi);
    }

    public List<int> GetAndClear()
    {
        var pois = BufferedPoiIds.ToList();
        BufferedPoiIds.Clear();
        return pois;
    }
}
