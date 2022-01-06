using System;
using UnityEngine;

[Serializable]
public class WaveConfiguration
{
    [SerializeField] private EnemyToSpawn enemyToSpawn;
    [SerializeField] private Vector3[] spawnPositions;
    [SerializeField] private Quaternion[] spawnRotations;
    [SerializeField] private int enemiesToSpawn;
    [SerializeField] private float timeToSpawn;

    public EnemyToSpawn EnemyToSpawn => enemyToSpawn;
    public Vector3[] SpawnPositions => spawnPositions;
    public Quaternion[] SpawnRotations => spawnRotations;
    public int EnemiesToSpawn => enemiesToSpawn;
    public float TimeToSpawn => timeToSpawn;
}
