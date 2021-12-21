public class VictoryState : IState
{
    public void Tick()
    {
        
    }

    public void OnEnter()
    {
        ServiceLocator.Instance.GetService<EventQueue>().EnqueueEvent(new VictoryEvent());
    }

    public void OnExit()
    {
        
    }
}
