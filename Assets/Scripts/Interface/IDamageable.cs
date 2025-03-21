namespace DeepeningPJ
{
    public interface IDamageable
    {
        float TakeDamage(float amount, int weaponRate, int weaponType);
        float TakeTrueDamage(float amount);
    }
}
