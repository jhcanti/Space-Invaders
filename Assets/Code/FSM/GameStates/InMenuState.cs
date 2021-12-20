public class InMenuState : IState, IEventObserver
{
    private readonly GameManager _gameManager;
    private EventQueue _eventQueue;
    private bool _startGamePressed;
    private bool _goToHighScoresPressed;
        
    public InMenuState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    
    public void OnEnter()
    {
        _startGamePressed = false;
        _goToHighScoresPressed = false;
        _eventQueue = ServiceLocator.Instance.GetService<EventQueue>();
        _eventQueue.Subscribe(EventIds.StartGamePressed, this);
        _eventQueue.Subscribe(EventIds.GoToHighScore, this);
        new LoadSceneCommand("Menu").Execute();
    }

    public void Tick()
    {
        if (_startGamePressed)
            _gameManager.CurrentGameState = GameStates.Playing;

        if (_goToHighScoresPressed)
            _gameManager.CurrentGameState = GameStates.InHighScores;
    }

    public void OnExit()
    {
        _startGamePressed = false;
        _goToHighScoresPressed = false;
        _eventQueue.Unsubscribe(EventIds.StartGamePressed, this);
        _eventQueue.Unsubscribe(EventIds.GoToHighScore, this);
    }

    public void Process(EventData eventData)
    {
        if (eventData.EventId == EventIds.StartGamePressed)
        {
            _startGamePressed = true;
        }

        if (eventData.EventId == EventIds.GoToHighScore)
        {
            _goToHighScoresPressed = true;
        }
    }
}
