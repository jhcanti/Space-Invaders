using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemiesConfiguration enemiesConfiguration;

    private LevelConfiguration _levelConfiguration;
    private EnemyFactory _enemyFactory;
    private EventQueue _eventQueue;
    private float _currentTime;
    private int _currentWaveIndex;
    private bool _canSpawn;

    private void Awake()
    {
        var instance = Instantiate(enemiesConfiguration);
        _enemyFactory = new EnemyFactory(instance);
    }

    private void Start()
    {
        _eventQueue = ServiceLocator.Instance.GetService<EventQueue>();
    }

    public void StartSpawn(LevelConfiguration levelConfiguration)
    {
        _levelConfiguration = levelConfiguration;
        _canSpawn = true;
    }

    public void StopAndReset()
    {
        _canSpawn = false;
        _currentWaveIndex = 0;
        _currentTime = 0;
    }

    private void Update()
    {
        if (!_canSpawn) return;

        if (_currentWaveIndex >= _levelConfiguration.WaveConfigurations.Length) return;

        _currentTime += Time.deltaTime;
        var waveConfiguration = _levelConfiguration.WaveConfigurations[_currentWaveIndex];
        if (waveConfiguration.TimeToSpawn > _currentTime) return;

        SpawnEnemy(waveConfiguration);
        _currentWaveIndex++;
        _currentTime = 0;

        if (_currentWaveIndex >= _levelConfiguration.WaveConfigurations.Length)
        {
            _eventQueue.EnqueueEvent(new AllEnemiesSpawnedEvent());
        }
    }

    private void SpawnEnemy(WaveConfiguration waveConfiguration)
    {
        foreach (var enemyToSpawn in waveConfiguration.EnemiesToSpawn)
        {
            var enemy = _enemyFactory.Create(enemyToSpawn.EnemyId.Value, enemyToSpawn.SpawnPosition, enemyToSpawn.SpawnRotation);
            enemy.Configure(enemyToSpawn.Health, enemyToSpawn.Speed, enemyToSpawn.FireRate, enemyToSpawn.PointsToAdd);
            _eventQueue.EnqueueEvent(new EnemySpawnEvent());
        }
    }
}
