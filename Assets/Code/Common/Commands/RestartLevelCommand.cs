public class RestartLevelCommand : ICommand
{
    public void Execute()
    {
        ServiceLocator.Instance.GetService<LevelSystem>().ResetAndStart();
        ServiceLocator.Instance.GetService<IScoreSystem>().SubtractLevelScore();
    }
}
