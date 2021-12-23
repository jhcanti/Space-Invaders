public class StopAndResetCommand : ICommand
{
    public void Execute()
    {
        ServiceLocator.Instance.GetService<EnemySpawner>().StopAndReset();
    }
}
