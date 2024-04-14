using UnityEngine;

public class MinionButtonAttackState : AMinionState
{
    private Vector2 _currentTarget;

    public MinionButtonAttackState(Minion minion) : base(minion)
    {
        _minion = minion;
        _currentTarget = GetRandomDestination();
    }

    public override void Update()
    {
        Run();
    }

    public override void DrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_minion.transform.position, _currentTarget);
    }

    private void Run()
    {
        Vector2 normalizedDirection = (_currentTarget - _minion.transform.position.ToVector2()).normalized;
        _minion.transform.Translate(Game.Data.UnitsPerSecondWhenWandering * Time.deltaTime * normalizedDirection);

        if(VectorHelper.Approximately(_minion.transform.position, _currentTarget))
        {
            _currentTarget = GetRandomDestination();
        }
    }

    private Vector2 GetRandomDestination()
    {
        // Vector2 destination;

        // do
        // {
        //    destination = _platform.center.ToVector2() + Random.insideUnitCircle * _platform.extents.ToVector2();
        // }
        // while(_button.Contains(destination));

        // return destination;
        return Vector2.zero;
    }
}
