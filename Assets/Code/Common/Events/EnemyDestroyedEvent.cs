public class EnemyDestroyedEvent : EventData
{
    public readonly int PointsToAdd;
    public readonly int InstanceId;

    public EnemyDestroyedEvent(int points, int instanceId) : base(EventIds.EnemyDestroyed)
    {
        PointsToAdd = points;
        InstanceId = instanceId;
    }
}
