using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IEventObserver
{
    [SerializeField] private LevelConfiguration levelConfiguration;
    [SerializeField] private EnemiesConfiguration enemiesConfiguration;

    private EnemyFactory _enemyFactory;
    private List<Enemy> _aliveEnemies;
    private float _currentTime;
    private int _currentConfigurationIndex;
    private bool _canSpawn;

    private void Awake()
    {
        var instance = Instantiate(enemiesConfiguration);
        _enemyFactory = new EnemyFactory(instance);
        _aliveEnemies = new List<Enemy>();
    }

    public void StartSpawn()
    {
        _canSpawn = true;
    }

    public void StopAndReset()
    {
        _aliveEnemies.Clear();
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
        for (var i = 0; i < waveConfiguration.EnemiesToSpawn.Length; i++)
        {
            var enemyToSpawn = waveConfiguration.EnemiesToSpawn[i];
            var enemy = _enemyFactory.Create(enemyToSpawn.EnemyId.Value, enemyToSpawn.SpawnPosition, enemyToSpawn.SpawnRotation);
            _aliveEnemies.Add(enemy);
            enemy.Configure(enemyToSpawn.Health, enemyToSpawn.Speed, enemyToSpawn.FireRate, enemyToSpawn.PointsToAdd);
        }
    }

    public void Process(EventData eventData)
    {
        
    }
}
