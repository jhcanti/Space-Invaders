using System;
using System.Threading.Tasks;

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
        _eventQueue.Subscribe(EventIds.NoContinue, this);
        _eventQueue.Subscribe(EventIds.ScoreUpdated, this);
        _eventQueue.EnqueueEvent(new GameOverEvent());
        new StopAndResetCommand().Execute();
    }

    public void OnExit()
    {
        _eventQueue.Unsubscribe(EventIds.RestartPressed, this);
        _eventQueue.Unsubscribe(EventIds.NoContinue, this);
        _eventQueue.Unsubscribe(EventIds.ScoreUpdated, this);
    }

    public void Process(EventData eventData)
    {
        if (eventData.EventId == EventIds.RestartPressed)
        {
            _restartButtonPressed = true;
        }

        if (eventData.EventId == EventIds.NoContinue)
        {
            var noContinueEvent = (NoContinueEvent) eventData;
            if (noContinueEvent.WaitForInput == false)
                Countdown();
        }

        if (eventData.EventId == EventIds.ScoreUpdated)
        {
            _gameManager.CurrentGameState = GameStates.InMenu;
        }
    }

    private async void Countdown()
    {
        await Task.Delay(TimeSpan.FromSeconds(5));
        _gameManager.CurrentGameState = GameStates.InMenu;
    }
}
