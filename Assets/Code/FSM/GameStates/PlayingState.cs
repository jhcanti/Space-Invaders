using UnityEngine;

public class PlayingState : IState, IEventObserver
{
    private readonly GameManager _gameManager;
    private EventQueue _eventQueue;
    private int _aliveEnemies;
    private bool _allEnemiesSpawned;

    public PlayingState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Tick()
    {
        if (_aliveEnemies == 0 && _allEnemiesSpawned)
        {
            _gameManager.CurrentGameState = GameStates.Victory;
        }
    }

    public void OnEnter()
    {
        _eventQueue = ServiceLocator.Instance.GetService<EventQueue>();
        _eventQueue.Subscribe(EventIds.EnemySpawned, this);
        _eventQueue.Subscribe(EventIds.EnemyDestroyed, this);
        _eventQueue.Subscribe(EventIds.AllEnemiesSpawned, this);
        _eventQueue.Subscribe(EventIds.PlayerDestroyed, this);
        _eventQueue.Subscribe(EventIds.BackToMenu, this);
        _aliveEnemies = 0;
        _allEnemiesSpawned = false;
    }

    public void OnExit()
    {
        ServiceLocator.Instance.GetService<PlayerInstaller>().SetPlayerInactive();
        _eventQueue.Unsubscribe(EventIds.EnemySpawned, this);
        _eventQueue.Unsubscribe(EventIds.EnemyDestroyed, this);
        _eventQueue.Unsubscribe(EventIds.AllEnemiesSpawned, this);
        _eventQueue.Unsubscribe(EventIds.PlayerDestroyed, this);
        _eventQueue.Unsubscribe(EventIds.BackToMenu, this);
    }

    public void Process(EventData eventData)
    {
        if (eventData.EventId == EventIds.EnemySpawned)
        {
            _aliveEnemies++;
        }

        if (eventData.EventId == EventIds.EnemyDestroyed)
        {
            _aliveEnemies--;
            if (_aliveEnemies < 0)
            {
                Debug.LogError("No pueden existir enemigos vivos en negativo");
            }
        }

        if (eventData.EventId == EventIds.AllEnemiesSpawned)
        {
            _allEnemiesSpawned = true;
        }

        if (eventData.EventId == EventIds.PlayerDestroyed)
        {
            _gameManager.CurrentGameState = GameStates.GameOver;
        }

        if (eventData.EventId == EventIds.BackToMenu)
        {
            new LoadSceneCommand("Menu").Execute();
        }
    }
}
