using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private LevelConfiguration levelConfiguration;
    [SerializeField] private EnemiesConfiguration enemiesConfiguration;

    private EnemyFactory _enemyFactory;
    private float _currentTime;
    private int _currentConfigurationIndex;
    private bool _canSpawn;

    private void Awake()
    {
        var instance = Instantiate(enemiesConfiguration);
        _enemyFactory = new EnemyFactory(instance);
    }

    public void StartSpawn()
    {
        _canSpawn = true;
    }

    public void StopAndReset()
    {
        _canSpawn = false;
        _currentConfigurationIndex = 0;
        _currentTime = 0;
    }

    private void Update()
    {
        if (!_canSpawn) return;

        if (_currentConfigurationIndex >= levelConfiguration.WaveConfigurations.Length) return;

        _currentTime += Time.deltaTime;
        var waveConfiguration = levelConfiguration.WaveConfigurations[_currentConfigurationIndex];
        if (waveConfiguration.TimeToSpawn > _currentTime) return;

        SpawnEnemy(waveConfiguration);
        _currentConfigurationIndex++;
        _currentTime = 0;

        if (_currentConfigurationIndex >= levelConfiguration.WaveConfigurations.Length)
        {
            //emitimos un evento de AllEnemiesSpawned
            Debug.Log("Todos los enemigos spawneados");
        }
    }

    private void SpawnEnemy(WaveConfiguration waveConfiguration)
    {
        foreach (var enemyToSpawn in waveConfiguration.EnemiesToSpawn)
        {
            var enemy = _enemyFactory.Create(enemyToSpawn.EnemyId.Value, enemyToSpawn.SpawnPosition, enemyToSpawn.SpawnRotation);
            enemy.Configure(enemyToSpawn.Health, enemyToSpawn.Speed, enemyToSpawn.FireRate, enemyToSpawn.PointsToAdd);
        }
    }
}
