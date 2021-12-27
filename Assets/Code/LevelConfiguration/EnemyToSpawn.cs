using UnityEngine;

[CreateAssetMenu(menuName = "Create/EnemyToSpawn")]
public class EnemyToSpawn : ScriptableObject
{
    [System.Serializable]
    public struct PowerUpProbability
    {
        public PowerUpId PowerUpId;
        public float Probability;
    }
    
    [SerializeField] private EnemyId enemyId;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private Quaternion spawnRotation;
    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private float fireRate;
    [SerializeField] private int pointsToAdd;
    [SerializeField] private PowerUpProbability[] _powerUpProbabilities;

    public EnemyId EnemyId => enemyId;
    public Vector3 SpawnPosition => spawnPosition;
    public Quaternion SpawnRotation => spawnRotation;
    public int Health => health;
    public float Speed => speed;
    public float FireRate => fireRate;
    public int PointsToAdd => pointsToAdd;
    public PowerUpProbability[] PowerUpProbabilities => _powerUpProbabilities;

}
