public class VictoryState : IState, IEventObserver
{
    private readonly GameManager _gameManager;
    private EventQueue _eventQueue;
    private PlayerInstaller _playerInstaller;

    public VictoryState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    
    public void Tick()
    {
    }

    public void OnEnter()
    {
        _eventQueue = ServiceLocator.Instance.GetService<EventQueue>();
        _playerInstaller = ServiceLocator.Instance.GetService<PlayerInstaller>();
        _playerInstaller.DestroyPlayer();
        _eventQueue.Subscribe(EventIds.NextLevel, this);
        _eventQueue.EnqueueEvent(new VictoryEvent());
    }

    public void OnExit()
    {
        _eventQueue.Unsubscribe(EventIds.NextLevel, this);
    }

    public void Process(EventData eventData)
    {
        if (eventData.EventId == EventIds.NextLevel)
        {
            _gameManager.CurrentGameState = GameStates.Playing;
        }
    }
}
