using UnityEngine;

public class MinionWanderingState : AMinionState
{
    private Vector2 _currentDestination;

    public MinionWanderingState(Minion minion) : base(minion)
    {
        _currentDestination = GetRandomDestination();
        _minion.Animator.SetTrigger("walk");
    }

    public override void Update()
    {
        Wander();
    }

    public override void UpdateTarget()
    {
        //TODO: is it ok to trust that there are adversaries?
        _minion.SetState(Minion.EState.AggressiveRunning);
    }

    public override void DrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_minion.transform.position, _currentDestination);
    }

    public override void CleanUp()
    {
        _minion.Animator.SetTrigger("idle");
    }

    private void Wander()
    {
        Vector2 normalizedDirection = (_currentDestination - _minion.transform.position.ToVector2()).normalized;
        _minion.SetSpriteDirection(normalizedDirection);
        float dot = Vector2.Dot(Vector2.right, normalizedDirection);
        float ellipsisFactor = Mathf.Lerp(Game.Environment.GetEllipsisFactor(), 1, Mathf.Abs(dot));
        _minion.transform.Translate(Game.Data.UnitsPerSecondWhenWandering * Time.deltaTime * ellipsisFactor * normalizedDirection);

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
        Vector2 ellipsisDestination = Game.Environment.GetEllipsisVector(destination);

        return ellipsisDestination;
    }
}
