using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    [Header("Spawning")]
    public float SpawningCircleRadius;
    public float SpawningCooldown;
    public Gradient ButtonGradient;

    [Header("Minions")]
    public float MovementUnitsPerSecond;
}
