using UnityEngine;

[CreateAssetMenu(menuName = "Create/EnemyToSpawn")]
public class EnemyToSpawn : ScriptableObject
{
    [SerializeField] private EnemyId enemyId;
    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private int pointsToAdd;
    [SerializeField] private PowerUpProbability[] _powerUpProbabilities;

    public EnemyId EnemyId => enemyId;
    public int Health => health;
    public float Speed => speed;
    public int PointsToAdd => pointsToAdd;
    public PowerUpProbability[] PowerUpProbabilities => _powerUpProbabilities;

}
