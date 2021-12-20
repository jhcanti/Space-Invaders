using UnityEngine;

public class UnityInputAdapter : IInput
{
    private ActionBindings _actionBindings;
        
    public void Configure(ActionBindings actionBindings)
    {
        _actionBindings = actionBindings;
    }


    public Vector2 GetDirection()
    {
        var horizontal = 0f;
        var vertical = 0f;
        if (Input.GetKey(_actionBindings.moveLeft)) horizontal = -1f; 
        if (Input.GetKey(_actionBindings.moveRight)) horizontal = 1f; 
        if (Input.GetKey(_actionBindings.moveUp)) vertical = 1f; 
        if (Input.GetKey(_actionBindings.moveDown)) vertical = -1f; 
             
        return new Vector2(horizontal, vertical).normalized;
    }

    public bool IsActionFirePressed()
    {
        return Input.GetKey(_actionBindings.fire);
    }

    public bool IsPausePressed()
    {
        return Input.GetKeyDown(_actionBindings.pause) ;
    }
}
