using UnityEngine;

public class MinionSpawner : ASpawner
{
    public override void SpawnMinions()
    {
        float signalIntensity01 = Game.Environment.GetSignalIntensity01();
        int minion_count = Mathf.RoundToInt(Mathf.Lerp( 1, 5, signalIntensity01));
        SpawnMinions(minion_count);

        Debug.Log($"Signal={signalIntensity01}, spawned {minion_count}");
    }

    protected override float GetNextAvailableSpawningTime()
    {
        return Time.time + Game.Data.SpawningCooldown;
    }

    protected override Vector2 GetSpawningPosition()
    {
        //TODO: return position on circle near the closest enemy
        float randomAngleRad = Random.Range(0, 2 * Mathf.PI);
        float radius = Game.Data.SpawningCircleRadius;

        Vector2 position = new(
            Game.Environment.Origin.position.x + radius * Mathf.Cos(randomAngleRad),
            Game.Environment.Origin.position.y * Game.Environment.GetPlatformEllipsisRatio() + radius * Mathf.Sin(randomAngleRad)
            );

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
