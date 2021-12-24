using UnityEngine;

[CreateAssetMenu(menuName = "Create/LevelConfiguration")]
public class LevelConfiguration : ScriptableObject
{
    [SerializeField] private WaveConfiguration[] waveConfigurations;
    [SerializeField] private Sprite parallaxBackground;

    public WaveConfiguration[] WaveConfigurations => waveConfigurations;
    public Sprite ParallaxBackground => parallaxBackground;
}
