using UnityEngine;

public class MinionSpawner : ASpawner
{
    public override void SpawnMinions()
    {
        float signalIntensity01 = Game.Environment.GetSignalIntensity01();
        int minionCount = Mathf.RoundToInt(Mathf.Lerp( Game.Data.MinimumCount, Game.Data.MaximumCount, signalIntensity01));
        SpawnMinions(minionCount);

        Debug.Log($"Signal={signalIntensity01}, spawned {minionCount}");
    }

    protected override float GetNextAvailableSpawningTime()
    {
        return Time.time + Game.Data.SpawningCooldown;
    }

    protected override Vector2 GetSpawningPosition()
    {
        if(_minionsManager.TryGetNearestMinion(Game.Environment.Origin.position, out Minion nearestMinion))
        {
            return GetClosestPointOnRadius(nearestMinion.transform.position, Game.Environment.Origin.position, Game.Data.SpawningCircleRadius);
        }
        else
        {
            return GetRandomPosition();
        }
    }

    private Vector2 GetClosestPointOnRadius(Vector2 targetPoint, Vector2 center, float radius)
    {
        Vector2 normalizedDirection = (targetPoint - center).normalized;
        Vector2 pointOnRadius = center + normalizedDirection * radius;

        return pointOnRadius;
    }

    private Vector2 GetRandomPosition()
    {
        float randomAngleRad = Random.Range(0, 2 * Mathf.PI);
        float radius = Game.Data.SpawningCircleRadius;

        Vector2 position = new(
            Game.Environment.Origin.position.x + radius * Mathf.Cos(randomAngleRad),
            Game.Environment.Origin.position.y + radius * Mathf.Sin(randomAngleRad)
            );

        position = Game.Environment.GetEllipsisVector(position);

        return position;
    }

    private void UpdateCooldownFeedbacks()
    {
        if(CanSpawn)
        {
            return;
        }

        Game.Environment.Button.color = Game.Data.ButtonGradient.Evaluate((Time.time - _nextAvailableTime + Game.Data.SpawningCooldown) / Game.Data.SpawningCooldown);
        //TODO: add spread effect when ready
    }

    private void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnMinions();
        }

        UpdateFistPosition();
    }

    private void UpdateFistPosition()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Game.Environment.Fist.transform.position = Game.Environment.Button.transform.position;
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            Game.Environment.Fist.transform.position = Game.Environment.FistUpPosition;
        }
    }

    private void Awake()
    {
        UpdateFistPosition();
    }

    private void Update()
    {
        HandleInput();
        UpdateCooldownFeedbacks();
    }
}
