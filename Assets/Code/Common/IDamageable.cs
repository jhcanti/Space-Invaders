public interface IDamageable
{
    void RecieveDamage(int amount);
    Teams Team { get; }
}