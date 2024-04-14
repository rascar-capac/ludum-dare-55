using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    [Header("Friendly spawning")]
    public float SpawningCircleRadius;
    public float SpawningCooldown;
    public Gradient ButtonGradient;
    public int MinimumCount;
    public int MaximumCount;

    [Header("Enemy spawning")]
    public float EnemySpawningPeriod;

    [Header("Movement")]
    public float UnitsPerSecondWhenWandering;
    public float UnitsPerSecondWhenRunning;

    [Header("Attack")]
    public float MinionDamagePerHit;
    public float MinionAttackPeriod;
    public float EnemyDamagePerHit;
    public float EnemyAttackPeriod;
    public float AttackDistance;
    public float EnemyDefensiveAttackDistance;

    [Header("Health")]
    public float MinionHealth;
    public float EnemyHealth;
    public float ButtonHealth;
    public Color DamageTakenColor;
    public Color DeadColor;
}
