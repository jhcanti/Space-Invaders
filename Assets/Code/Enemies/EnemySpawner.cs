using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemiesConfiguration enemiesConfiguration;
    [SerializeField] private Transform enemiesParentTransform;

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
            _canSpawn = false;
        }
    }

    private void SpawnEnemy(WaveConfiguration waveConfiguration)
    {
        for (var i = 0; i < waveConfiguration.EnemiesToSpawn; i++)
        {
            var enemyToSpawn = waveConfiguration.EnemyToSpawn;
            var enemy = _enemyFactory.Create(enemyToSpawn.EnemyId.Value, waveConfiguration.SpawnPositions[i],
                waveConfiguration.SpawnRotations[i], enemiesParentTransform);
            enemy.Configure(enemyToSpawn.Health, enemyToSpawn.Speed, enemyToSpawn.PointsToAdd, enemyToSpawn.PowerUpProbabilities);
            _eventQueue.EnqueueEvent(new EnemySpawnEvent());
        }
    }
}
