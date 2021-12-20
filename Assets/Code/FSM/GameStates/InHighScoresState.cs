public class InHighScoresState : IState, IEventObserver
{
    private readonly GameManager _gameManager;
    private bool _backButtonPressed;

    public InHighScoresState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void OnEnter()
    {
        _backButtonPressed = false;
        ServiceLocator.Instance.GetService<EventQueue>().Subscribe(EventIds.BackToMenu, this);
        new LoadSceneCommand("HighScores").Execute();
    }

    public void Tick()
    {
        if (_backButtonPressed)
            _gameManager.CurrentGameState = GameStates.InMenu;
    }

    public void OnExit()
    {
        _backButtonPressed = false;
        ServiceLocator.Instance.GetService<EventQueue>().Unsubscribe(EventIds.BackToMenu, this);
    }

    public void Process(EventData eventData)
    {
        if (eventData.EventId == EventIds.BackToMenu)
        {
            _backButtonPressed = true;
        }
    }
}
