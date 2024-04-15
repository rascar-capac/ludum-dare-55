using System;
using UnityEngine;

public class EnemyMinionSpawner : ASpawner
{
    public override void SpawnMinions()
    {
        SpawnMinions(1);
    }

    protected override float GetNextAvailableSpawningTime()
    {
        float period = Mathf.Lerp(Game.Data.MinEnemySpawningPeriod, Game.Data.MaxEnemySpawningPeriod, Time.time / Game.Data.TotalCurveTime);
        period += period * UnityEngine.Random.Range(-0.2f, 0.2f);

        return Time.time + Mathf.Clamp(period, Game.Data.MaxEnemySpawningPeriod, Game.Data.MinEnemySpawningPeriod);
    }

    protected override Vector2 GetSpawningPosition()
    {
        float outerRadius = Game.Environment.Platform.extents.x;
        float innerRadius = outerRadius * 0.9f;
        Vector2 positionInBelt = VectorHelper.GetRandomPositionInBelt(Game.Environment.Origin.position, innerRadius, outerRadius);
        Vector2 ellipsisBelt = Game.Environment.GetEllipsisVector(positionInBelt);

        return ellipsisBelt;
    }

    private void Update()
    {
        SpawnMinions();
    }
}
