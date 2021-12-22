public class GameOverState : IState, IEventObserver
{
    private readonly GameManager _gameManager;
    private EventQueue _eventQueue;
    private bool _menuButtonPressed;

    public GameOverState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    
    public void Tick()
    {
        if (_menuButtonPressed)
            _gameManager.CurrentGameState = GameStates.InMenu;
    }

    public void OnEnter()
    {
        _menuButtonPressed = false;
        _eventQueue = ServiceLocator.Instance.GetService<EventQueue>();
        _eventQueue.Subscribe(EventIds.BackToMenu, this);
        _eventQueue.EnqueueEvent(new GameOverEvent());
    }

    public void OnExit()
    {
        _eventQueue.Unsubscribe(EventIds.BackToMenu, this);
    }

    public void Process(EventData eventData)
    {
        if (eventData.EventId == EventIds.BackToMenu)
        {
            _menuButtonPressed = true;
        }
    }
}
