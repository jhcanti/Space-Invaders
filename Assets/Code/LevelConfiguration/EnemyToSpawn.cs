using UnityEngine;

[CreateAssetMenu(menuName = "Create/EnemyToSpawn")]
public class EnemyToSpawn : ScriptableObject
{
    [SerializeField] private EnemyId enemyId;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private Quaternion spawnRotation;
    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private float fireRate;
    [SerializeField] private int pointsToAdd;

    public EnemyId EnemyId => enemyId;
    public Vector3 SpawnPosition => spawnPosition;
    public Quaternion SpawnRotation => spawnRotation;
    public int Health => health;
    public float Speed => speed;
    public float FireRate => fireRate;
    public int PointsToAdd => pointsToAdd;

}
