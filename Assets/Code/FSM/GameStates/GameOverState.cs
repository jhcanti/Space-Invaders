public class GameOverState : IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
        ServiceLocator.Instance.GetService<EventQueue>().EnqueueEvent(new GameOverEvent());
    }

    public void OnExit()
    {
    }
}
