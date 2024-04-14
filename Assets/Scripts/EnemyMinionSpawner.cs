using UnityEngine;

public class EnemyMinionSpawner : ASpawner
{
    public override void SpawnMinions()
    {
        SpawnMinions(1);
    }

    protected override float GetNextAvailableSpawningTime()
    {
        return Time.time + Game.Data.EnemySpawningPeriod;
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
