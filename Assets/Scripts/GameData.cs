using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    [Header("Friendly spawning")]
    public float SpawningCircleRadius;
    public float SpawningCooldown;
    public Gradient ButtonGradient;

    [Header("Enemy spawning")]
    public float EnemySpawningPeriod;

    [Header("Minions")]
    public float UnitsPerSecondWhenWandering;
    public float UnitsPerSecondWhenRunning;
}
