public class RestartLevelCommand : ICommand
{
    public void Execute()
    {
        ServiceLocator.Instance.GetService<LevelSystem>().ResetAndStart();
        // quizas el ScoreSystem deberia resetear los puntos conseguidos en el nivel fallido
    }
}
