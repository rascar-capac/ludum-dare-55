using UnityEngine;

public class MinionButtonAttackingState : AMinionState
{
    private Target _currentTarget;
    private float _nextAttackTime;

    public MinionButtonAttackingState(Minion minion, Target target = null) : base(minion)
    {
        if(target == null && !minion.TryGetTarget(out _currentTarget))
        {
            _minion.SetState(Minion.EState.Wandering);
        }
        else
        {
            _currentTarget = target;
        }

        if(_currentTarget != null)
        {
            _currentTarget.OnDied.AddListener(Target_OnDied);
        }
    }

    public override void Update()
    {
        if(Time.time > _nextAttackTime)
        {
            Attack();
            _nextAttackTime = Time.time + (_minion.IsEnemy ? Game.Data.EnemyAttackPeriod : Game.Data.MinionAttackPeriod);
        }
    }

    public override void CleanUp()
    {
        if(_currentTarget != null )
        {
            _currentTarget.OnDied.RemoveListener(Target_OnDied);
        }
    }

    private void Attack()
    {
        _currentTarget.TakeDamage(_minion.IsEnemy ? Game.Data.EnemyDamagePerHit : Game.Data.MinionDamagePerHit);
    }

    private void Target_OnDied()
    {
        _minion.SetState(Minion.EState.AggressiveRunning);
        _currentTarget.OnDied.RemoveListener(Target_OnDied);
        _currentTarget = null;
    }
}
