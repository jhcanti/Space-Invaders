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
        _lineRenderer.enabled = true;
        _isActive = true;
        StartCoroutine(Countdown(100 / id.DurabilityInSeconds));
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
        _lineRenderer.enabled = false;        
    }

    private IEnumerator Countdown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        DoDeactivate();
    }
}
