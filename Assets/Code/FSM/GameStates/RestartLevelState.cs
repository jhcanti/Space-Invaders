public class RestartLevelState : IState, IEventObserver
{
    private readonly GameManager _gameManager;

    public RestartLevelState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    
    public void Tick()
    {
        
    }

    public void OnEnter()
    {
        ServiceLocator.Instance.GetService<EventQueue>().Subscribe(EventIds.RestartLevelComplete, this);
        new RestartLevelCommand().Execute();
    }

    public void OnExit()
    {
        ServiceLocator.Instance.GetService<EventQueue>().Unsubscribe(EventIds.RestartLevelComplete, this);
    }

    public void Process(EventData eventData)
    {
        if (eventData.EventId == EventIds.RestartLevelComplete)
        {
            _gameManager.CurrentGameState = GameStates.Playing;
        }
    }
}
