using System;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public AMinionState CurrentState {get; private set;}
    public Transform Transform => transform;
    public Target Health => _health;
    public bool IsEnemy => _isEnemy;
    public bool IsDead => Health.IsDead;
    public Animator Animator => _animator;

    [SerializeField] private bool _isEnemy;
    [SerializeField] Target _health;
    [SerializeField] private Animator _animator;

    private MinionsManager _manager;

    public void Initialize(MinionsManager manager)
    {
        _manager = manager;
        SetState(EState.AggressiveRunning);
        Health.Initialize(IsEnemy ? Game.Data.EnemyHealth : Game.Data.MinionHealth);
    }

    public void SetState(EState stateType)
    {
        CurrentState?.CleanUp();
        CurrentState = stateType switch
        {
            EState.None => null,
            EState.Wandering => new MinionWanderingState(this),
            EState.AggressiveRunning => new MinionAggressiveRunningState(this),
            _ => throw new NotImplementedException()
        };
    }

    public void SetState(AMinionState newState)
    {
        CurrentState?.CleanUp();
        CurrentState = newState;
    }

    public bool TryGetTarget(out Target target)
    {
        bool nearestMinionFound = _manager.TryGetNearestMinion(this, out Minion nearestMinion);

        if(_isEnemy)
        {
            if(nearestMinionFound && IsAtDefenseDistance(nearestMinion._health))
            {
                target = nearestMinion._health;

                return true;
            }

            if(!Game.Environment.ButtonBox.IsDead)
            {
                target = Game.Environment.ButtonBox;

                return true;
            }

            target = null;

            return false;
        }

        target = nearestMinionFound ? nearestMinion.Health : null;

        return nearestMinionFound;
    }

    public void UpdateTarget()
    {
        if(CurrentState == null)
        {
            return;
        }

        CurrentState.UpdateTarget();
    }

    public bool IsAtDefenseDistance(Target target)
    {
        return Vector3.Distance(transform.position, target.SpriteRenderer.bounds.ClosestPoint(transform.position)) < Game.Data.EnemyDefensiveAttackDistance;
    }

    public void SetSpriteDirection(Vector2 normalizedDirection)
    {
        _health.SpriteRenderer.flipX = Vector2.Dot(normalizedDirection, Vector2.right) < 0;
    }

    public bool CanAttack(Target target)
    {
        return Vector3.Distance(transform.position, target.SpriteRenderer.bounds.ClosestPoint(transform.position)) < Game.Data.AttackDistance;
    }

    private void Health_OnDied()
    {
        SetState(EState.None);
        _animator.SetTrigger("die");
    }

    private void Awake()
    {
        Health.OnDied.AddListener(Health_OnDied);
    }

    private void Update()
    {
        if(CurrentState == null)
        {
            return;
        }

        CurrentState.Update();
    }

    private void OnDrawGizmos()
    {
        if(CurrentState == null)
        {
            return;
        }

        CurrentState.DrawGizmos();
    }

    private void OnDestroy()
    {
        Health.OnDied.RemoveListener(Health_OnDied);
    }

    public enum EState
    {
        None = -1,
        Wandering = 0,
        AggressiveRunning = 1,
        Attacking = 2,
    }
}
