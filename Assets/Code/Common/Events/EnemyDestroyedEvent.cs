public class EnemyDestroyedEvent : EventData
{
    public readonly int PointsToAdd;

    public EnemyDestroyedEvent(int points) : base(EventIds.EnemyDestroyed)
    {
        PointsToAdd = points;
    }
}
