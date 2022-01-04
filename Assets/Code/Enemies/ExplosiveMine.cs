using DG.Tweening;
using UnityEngine;

public class ExplosiveMine : Enemy
{
    [SerializeField] private float radiusDetection;
    [SerializeField] private float radiusExplosion;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float durationBlink;
    [SerializeField] private int loops;
    [SerializeField] private int damageByExplosion;

    private SpriteRenderer _myRenderer;
    private bool _isActive;
    private Sequence _sequence; 
    
    protected override void DoInit()
    {
        _myRenderer = GetComponent<SpriteRenderer>();
        _isActive = false;
        Rb.velocity = MyTransform.right * Speed;
    }

    protected override void DoMove()
    {
        if (_isActive) return;
        
        var hit = Physics2D.CircleCast(MyTransform.position, radiusDetection,
            MyTransform.right, 0f, layerMask);
        if (hit.collider == null) return;

        _isActive = true;
        var currentColor = _myRenderer.color;
        _sequence = DOTween.Sequence();
        _sequence.Append(_myRenderer.DOColor(Color.white, durationBlink));
        _sequence.Append(_myRenderer.DOColor(currentColor, durationBlink));
        _sequence.SetLoops(loops);
        _sequence.OnComplete(() =>
        {
            ApplyDamageByExplosion();
            DestroyEnemy(0);
        });
    }

    private void ApplyDamageByExplosion()
    {
        var hit = Physics2D.CircleCast(MyTransform.position, radiusExplosion,
            MyTransform.right, 0f, layerMask);
        if (hit.collider == null) return;
        
        var damageable = hit.collider.GetComponent<IDamageable>();

        damageable?.ReceiveDamage(damageByExplosion);
    }

    protected override void DoDestroy()
    {
        if (_sequence.IsActive())
        {
            _sequence.Kill();
        }
    }

    protected override void DoTryShoot()
    {

    }

    private void OnDrawGizmos()
    {
        if (_isActive == false)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(MyTransform.position, radiusDetection);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(MyTransform.position, radiusExplosion);
        }
    }
}
