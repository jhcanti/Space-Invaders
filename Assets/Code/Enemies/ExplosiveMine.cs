using DG.Tweening;
using UnityEngine;

public class ExplosiveMine : Enemy
{
    [SerializeField] private float radiusDetection;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float durationBlink;
    [SerializeField] private int loops;

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
        _sequence.OnComplete(() => DestroyEnemy(0));
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
        Gizmos.DrawWireSphere(MyTransform.position, 1.5f);
    }
}
