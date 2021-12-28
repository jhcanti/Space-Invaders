using UnityEngine;

public class ChangeProjectilePowerUp : PowerUp
{
    [SerializeField] private ProjectileId[] projectiles;

    protected override void DoInit()
    {
        Rb.velocity = -MyTransform.right * Speed;
    }

    protected override void DoMove()
    {
    }

    protected override void DoOnTriggerEnter(PlayerMediator player)
    {
        var projectileId = projectiles[Random.Range(0, projectiles.Length)];
        player.SetProjectile(projectileId);
        DoDestroy();
    }

    protected override void DoDestroy()
    {
        Destroy(gameObject);
    }
}
