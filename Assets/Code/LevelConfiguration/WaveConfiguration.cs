using System;
using UnityEngine;

[Serializable]
public class WaveConfiguration
{
    [SerializeField] private EnemyToSpawn[] enemiesToSpawn;
    [SerializeField] private float timeToSpawn;

    public EnemyToSpawn[] EnemiesToSpawn => enemiesToSpawn;
    public float TimeToSpawn => timeToSpawn;
}
