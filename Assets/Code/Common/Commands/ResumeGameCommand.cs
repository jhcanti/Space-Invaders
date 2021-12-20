using UnityEngine;

public class ResumeGameCommand : ICommand
{
    public void Execute()
    {
        Time.timeScale = 1;
    }
}
