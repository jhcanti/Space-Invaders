using UnityEngine;

[CreateAssetMenu(menuName = "Create/LevelConfiguration")]
public class LevelConfiguration : ScriptableObject
{
    [SerializeField] private WaveConfiguration[] waveConfigurations;

    public WaveConfiguration[] WaveConfigurations => waveConfigurations;
}
