public interface IDamageable
{
    void ReceiveDamage(int amount);
    Teams Team { get; }
}