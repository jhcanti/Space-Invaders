using System.Collections;
using UnityEngine;

public class LaserProjectile : Projectile
{
    [SerializeField] private float distanceLaser;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float timeToRecharge;
    [SerializeField] private float laserDuration;

    private Transform _laserOrigin;
    private LineRenderer _lineRenderer;
    private bool _isActive;
    
    
    protected override void DoInit()
    {
        _laserOrigin = ShootPoint;
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = true;
        _isActive = true;
    }

    protected override void DoMove()
    {
        if (_isActive == false) return;

        laserDuration -= Time.fixedDeltaTime;
        if (laserDuration <= 0)
        {
            _isActive = false;
            _lineRenderer.enabled = false;
            StartCoroutine(RechargeLaser(timeToRecharge));
        }
        
        var hit = Physics2D.Raycast(_laserOrigin.position, _laserOrigin.right, distanceLaser,
            layerMask);
        if (hit.collider == null)
        {
            DrawRay(_laserOrigin.position, MyTransform.right, distanceLaser);    
        }
        else
        {
            DrawRay(_laserOrigin.position, MyTransform.right, hit.distance);
        }
    }

    private void DrawRay(Vector2 origin, Vector2 direction, float distance)
    {
        _lineRenderer.SetPosition(0, origin);
        var end = new Vector2(origin.x + distance, origin.y);
        _lineRenderer.SetPosition(1, end);
    }

    private IEnumerator RechargeLaser(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ServiceLocator.Instance.GetService<EventQueue>().EnqueueEvent(new CanShootEvent());
        DeactivateProjectile();
    }
    
    protected override void DoDeactivate()
    {
        
    }
}
