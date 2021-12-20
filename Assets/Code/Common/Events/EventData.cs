public abstract class EventData
{
    public readonly EventIds EventId;

    protected EventData(EventIds eventId)
    {
        EventId = eventId;
    }
}
