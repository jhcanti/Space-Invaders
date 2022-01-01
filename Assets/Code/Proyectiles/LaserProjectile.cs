using System.Collections;
using UnityEngine;

public class LaserProjectile : Projectile
{
    [SerializeField] private float distanceLaser;
    [SerializeField] private LayerMask layerMask;

    private Transform _laserOrigin;
    private LineRenderer _lineRenderer;
    private bool _isActive;
    
    
    protected override void DoInit()
    {
        _laserOrigin = ShootPoint;
        _lineRenderer = GetComponent<LineRenderer>();
        _isActive = true;
        StartCoroutine(Countdown(id.DurabilityInSeconds));
    }

    protected override void DoMove()
    {
        if (_isActive == false) return;

        var hit = Physics2D.Raycast(_laserOrigin.position, _laserOrigin.right, distanceLaser,
            layerMask);
        if (hit.collider == null)
        {
            DrawRay(_laserOrigin.position, distanceLaser);    
        }
        else
        {
            DrawRay(_laserOrigin.position, hit.distance);
            var damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable == null) return;

            if (damageable.Team != Team)
            {
                damageable.ReceiveDamage(Damage);
            }
        }
    }

    private void DrawRay(Vector2 origin, float distance)
    {
        _lineRenderer.SetPosition(0, origin);
        var end = new Vector2(origin.x + distance, origin.y);
        _lineRenderer.SetPosition(1, end);
    }

    protected override void DoDeactivate()
    {
        _isActive = false;
    }

    private IEnumerator Countdown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        DeactivateProjectile();
    }
}
