using UnityEngine;

public class PowerUpFactory
{
    private readonly PowerUpsConfiguration _powerUpsConfiguration;

    public PowerUpFactory(PowerUpsConfiguration powerUpsConfiguration)
    {
        _powerUpsConfiguration = powerUpsConfiguration;
    }

    public PowerUp Create(string id, Vector3 position, Quaternion rotation)
    {
        var prefab = _powerUpsConfiguration.GetPowerUpById(id);
        var powerUp = Object.Instantiate(prefab, position, rotation);
        return powerUp;
    }
}
