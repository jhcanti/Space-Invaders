using UnityEngine;

public class Turret : Enemy
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform cannon;
    private Vector2 _direction;
    
    
    protected override void DoInit()
    {
        _direction = -MyTransform.right;
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
        Rb.MovePosition(Rb.position + _direction * (Speed * Time.fixedDeltaTime));
        var aimDirection = (Player.position - transform.position).normalized;
        var angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        cannon.rotation = Quaternion.Euler(0f, 0f, angle - 180);
        animator.SetFloat("Angle", angle);
    }

    protected override void DoDestroy()
    {
        
    }

}
