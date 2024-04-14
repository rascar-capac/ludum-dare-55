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

        _minion.Animator.SetTrigger("idle");
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

        _minion.Animator.SetTrigger("idle");
    }

    private void Attack()
    {
        Vector2 normalizedDirection = (_currentTarget.transform.position - _minion.transform.position).normalized;
        _minion.SetSpriteDirection(normalizedDirection);
        _currentTarget.TakeDamage(_minion.IsEnemy ? Game.Data.EnemyDamagePerHit : Game.Data.MinionDamagePerHit);
        _minion.Animator.SetTrigger("attack");
    }

    private void Target_OnDied()
    {
        _minion.SetState(Minion.EState.AggressiveRunning);
        _currentTarget.OnDied.RemoveListener(Target_OnDied);
        _currentTarget = null;
    }
}
