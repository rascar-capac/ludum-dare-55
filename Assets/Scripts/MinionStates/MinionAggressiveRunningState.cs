using UnityEngine;

public class MinionAggressiveRunningState : AMinionState
{
    private Target _currentTarget;

    public MinionAggressiveRunningState(Minion minion) : base(minion) { }

    public override void Update()
    {
        if(_currentTarget != null)
        {
            _currentTarget.OnDied.RemoveListener(Target_OnDied);
        }

        if(_minion.TryGetTarget(out _currentTarget))
        {
            if(_minion.CanAttack(_currentTarget))
            {
                _minion.SetState(new MinionButtonAttackingState(_minion, _currentTarget));
            }
            else
            {
                Run();
                _currentTarget.OnDied.AddListener(Target_OnDied);
            }
        }
        else
        {
            _minion.SetState(Minion.EState.Wandering);
        }
    }

    public override void CleanUp()
    {
        if(_currentTarget != null )
        {
            _currentTarget.OnDied.RemoveListener(Target_OnDied);
        }
    }

    public override void DrawGizmos()
    {
        if(_currentTarget == null)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(_minion.transform.position, _currentTarget.transform.position);
    }

    private void Run()
    {
        Vector2 normalizedDirection = (_currentTarget.transform.position - _minion.transform.position).normalized;
        float dot = Vector2.Dot(Vector2.right, normalizedDirection);
        float ellipsis_factor = Mathf.Lerp(Game.Environment.GetEllipsisFactor(), 1, Mathf.Abs(dot));
        _minion.transform.Translate(Game.Data.UnitsPerSecondWhenRunning * Time.deltaTime * ellipsis_factor * normalizedDirection);
    }

    private void Target_OnDied()
    {
        _currentTarget.OnDied.RemoveListener(Target_OnDied);
        _currentTarget = null;
    }
}
