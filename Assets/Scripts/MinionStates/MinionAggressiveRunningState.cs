using UnityEngine;

public class MinionAggressiveRunState : AMinionState
{
    private Vector2 _currentTargetPosition;

    public MinionAggressiveRunState(Minion minion) : base(minion) { }

    public override void Update()
    {
        if(_minion.TryGetTargetPosition(out _currentTargetPosition))
        {
            Run();
        }
        else
        {
            _minion.SetState(Minion.EState.Wandering);
        }
    }

    public override void DrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_minion.transform.position, _currentTargetPosition);
    }

    private void Run()
    {
        Vector2 normalizedDirection = (_currentTargetPosition - _minion.transform.position.ToVector2()).normalized;
        float dot = Vector2.Dot(Vector2.right, normalizedDirection);
        float ellipsis_factor = Mathf.Lerp(Game.Environment.GetPlatformEllipsisRatio() * 0.1f, 1, Mathf.Abs(dot));
        _minion.transform.Translate(Game.Data.UnitsPerSecondWhenRunning * Time.deltaTime * ellipsis_factor * normalizedDirection);
    }
}
