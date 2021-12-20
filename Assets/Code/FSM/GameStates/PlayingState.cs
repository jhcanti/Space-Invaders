public class PlayingState : IState, IEventObserver
{
    private readonly GameManager _gameManager;

    public PlayingState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Tick()
    {
    }

    public void OnEnter()
    {
        new LoadSceneCommand("Game").Execute();
    }

    public void OnExit()
    {
    }

    public void Process(EventData eventData)
    {
            
    }
}
