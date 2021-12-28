public class NoContinueEvent : EventData
{
    public bool WaitForInput; 
    public NoContinueEvent(bool wait) : base(EventIds.NoContinue)
    {
        WaitForInput = wait;
    }
}
