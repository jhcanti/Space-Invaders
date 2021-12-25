public class VictoryState : IState, IEventObserver
{
    private readonly GameManager _gameManager;
    private EventQueue _eventQueue;
    private bool _nextLevel;

    public VictoryState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    
    public void Tick()
    {
        if (_nextLevel)
            _gameManager.CurrentGameState = GameStates.Playing;
    }

    public void OnEnter()
    {
        _nextLevel = false;
        _eventQueue = ServiceLocator.Instance.GetService<EventQueue>();
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
            _nextLevel = true;
        }
    }
}
