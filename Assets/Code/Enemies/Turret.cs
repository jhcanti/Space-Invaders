using System;
using UnityEngine;

public class Turret : Enemy
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform cannon;
    
    
    
    protected override void DoInit()
    {
        Rb.velocity = -MyTransform.right * Speed;
    }

    protected override void DoTryShoot()
    {
        if (Vector2.Distance(Player.position, transform.position) <= MinimumDistanceToShoot)
        {
            WeaponController.TryShoot();
        }
    }

    protected override void DoMove()
    {
        var aimDirection = (Player.position - transform.position).normalized;
        var angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        cannon.rotation = Quaternion.Euler(0f, 0f, angle - 180);
        animator.SetFloat("Angle", angle);
    }

    protected override void DoDestroy()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, MinimumDistanceToShoot);
    }
}
