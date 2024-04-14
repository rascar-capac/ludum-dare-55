using UnityEngine;

public class MinionWanderingState : AMinionState
{
    private Vector2 _currentDestination;
    private Minion _minion;

    public MinionWanderingState(Minion minion)
    {
        _minion = minion;
        _currentDestination = GetRandomDestination();
    }

    public override void Update()
    {
        Wander();
    }

    public override void DrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_minion.transform.position, _currentDestination);
    }

    private void Wander()
    {
        Vector2 normalizedDirection = (_currentDestination - _minion.transform.position.ToVector2()).normalized;
        float dot = Vector2.Dot(Vector2.right, normalizedDirection);
        float ellipsis_factor = Mathf.Lerp(Game.Environment.GetPlatformEllipsisRatio() * 0.1f, 1, Mathf.Abs(dot));
        _minion.transform.Translate(Game.Data.MovementUnitsPerSecond * Time.deltaTime * ellipsis_factor * normalizedDirection);

        if(VectorHelper.Approximately(_minion.transform.position, _currentDestination))
        {
            _currentDestination = GetRandomDestination();
        }
    }

    private Vector2 GetRandomDestination()
    {
        float innerRadius = Game.Environment.Origin.position.x + Game.Data.SpawningCircleRadius;
        float outerRadius = Game.Environment.Platform.extents.x;
        Vector2 destination = VectorHelper.GetRandomPositionInBelt(Game.Environment.Origin.position, innerRadius, outerRadius);
        Vector2 ellipsisDestination = new(destination.x, destination.y * Game.Environment.GetPlatformEllipsisRatio());

        return ellipsisDestination;
    }
}
