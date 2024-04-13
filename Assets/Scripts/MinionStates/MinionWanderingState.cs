using UnityEngine;

public class MinionWanderingState : AMinionState
{
    private Vector2 _currentDestination;
    private Minion _minion;
    private Bounds _platform;
    private Bounds _button;

    public MinionWanderingState(Minion minion, Bounds platform, Bounds button)
    {
        _minion = minion;
        _platform = platform;
        _button = button;
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
        _minion.transform.Translate(Game.Data.MovementUnitsPerSecond * Time.deltaTime * normalizedDirection);

        if(VectorExtensions.Approximately(_minion.transform.position, _currentDestination))
        {
            _currentDestination = GetRandomDestination();
        }
    }

    private Vector2 GetRandomDestination()
    {
        Vector2 destination;

        do
        {
           destination = _platform.center.ToVector2() + Random.insideUnitCircle * _platform.extents.ToVector2();
        }
        while(_button.Contains(destination));

        return destination;
    }
}
