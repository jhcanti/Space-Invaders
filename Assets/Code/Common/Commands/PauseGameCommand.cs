using UnityEngine;

public class PauseGameCommand : ICommand
{
    public void Execute()
    {
        Time.timeScale = 0;
    }
}
