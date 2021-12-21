using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour
{
    public Teams Team { get; private set; }
    public string Id => id.Value;

    [SerializeField] private ProjectileId id;
    [SerializeField] protected float Speed;
    [SerializeField] private float timeToDeactivate;
    [SerializeField] private int damage;

    protected Rigidbody2D Rb;
    protected Collider2D Collider2D;
    protected Transform MyTransform;
    protected bool Active;


    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Collider2D = GetComponent<Collider2D>();
    }

    public void Init(Vector3 position, Quaternion rotation, Teams team)
    {
        MyTransform = transform;
        Active = true;
        Team = team;
        DoInit(position, rotation);
        StartCoroutine(DeactivateIn(timeToDeactivate));
    }

    protected abstract void DoInit(Vector3 position, Quaternion rotation);

    private void FixedUpdate()
    {
        DoMove();
    }

    protected abstract void DoMove();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageable = collision.GetComponent<IDamageable>();
        if (damageable == null) return;

        if (damageable.Team != Team)
        {
            damageable.ReceiveDamage(damage);
            DeactivateProjectile();
        }
            
    }
    
    private IEnumerator DeactivateIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        DeactivateProjectile();
    }

    private void DeactivateProjectile()
    {
        DoDeactivate();
        Active = false;
        gameObject.SetActive(false);
    }

    protected abstract void DoDeactivate();
}