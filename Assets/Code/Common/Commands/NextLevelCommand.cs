public class NextLevelCommand : ICommand
{
    public void Execute()
    {
        ServiceLocator.Instance.GetService<LevelSystem>().NextLevel();
        ServiceLocator.Instance.GetService<EnemySpawner>().StopAndReset();
        ServiceLocator.Instance.GetService<IScoreSystem>().ResetLevelScore();
    }
}
