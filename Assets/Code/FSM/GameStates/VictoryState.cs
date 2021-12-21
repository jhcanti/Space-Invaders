using UnityEngine;

public class VictoryState : IState
{
    public void Tick()
    {
        
    }

    public void OnEnter()
    {
        Debug.Log("Victory!!");
    }

    public void OnExit()
    {
        
    }
}
