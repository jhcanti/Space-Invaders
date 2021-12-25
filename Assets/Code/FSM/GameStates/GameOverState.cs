public class GameOverState : IState, IEventObserver
{
    private readonly GameManager _gameManager;
    private EventQueue _eventQueue;
    private bool _restartButtonPressed;

    public GameOverState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    
    public void Tick()
    {
        if (_restartButtonPressed)
            _gameManager.CurrentGameState = GameStates.RestartLevel;
    }

    public void OnEnter()
    {
        _restartButtonPressed = false;
        _eventQueue = ServiceLocator.Instance.GetService<EventQueue>();
        _eventQueue.Subscribe(EventIds.RestartPressed, this);
        _eventQueue.EnqueueEvent(new GameOverEvent());
        new StopAndResetCommand().Execute();
    }

    public void OnExit()
    {
        _eventQueue.Unsubscribe(EventIds.RestartPressed, this);
    }

    public void Process(EventData eventData)
    {
        if (eventData.EventId == EventIds.RestartPressed)
        {
            _restartButtonPressed = true;
        }
    }
}
