using System.Collections;
using UnityEngine;

public class FragmentationProjectile : Projectile
{
    private Vector2 _direction;
    private bool _hasDetonated;
    
    
    public void Configure(Vector2 direction, float timeToDetonate)
    {
        _direction = direction;
        _hasDetonated = false;
        StartCoroutine(WaitForDetonate(timeToDetonate));
    }
    
    protected override void DoInit()
    {
        Rb.velocity = MyTransform.right * Speed;
    }

    protected override void DoMove()
    {
        if (_hasDetonated)
        {
            Rb.velocity = _direction * Speed;
        }
    }

    protected override void DoDeactivate()
    {
        
    }

    private IEnumerator WaitForDetonate(float time)
    {
        yield return new WaitForSeconds(time);
        _hasDetonated = true;
    }
}
