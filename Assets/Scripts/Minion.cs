using System;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public AMinionState CurrentState {get; private set;}
    public Transform Transform => transform;
    public Target Health => _health;
    public bool IsEnemy => _isEnemy;
    public bool IsDead => Health.IsDead;

    [SerializeField] private bool _isEnemy;
    [SerializeField] Target _health;

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
        if(_isEnemy)
        {
            //TODO: return nearest minion if in detection area
            target = Game.Environment.ButtonBox;

            return true;
        }

        bool nearest_minion_found = _manager.TryGetNearestMinion(this, out Minion nearestMinion);
        target = nearest_minion_found ? nearestMinion.Health : null;

        return nearest_minion_found;
    }

    public void UpdateTarget()
    {
        if(CurrentState == null)
        {
            return;
        }

        CurrentState.UpdateTarget();
    }

    public bool CanAttack(Target target)
    {
        return Vector3.Distance(transform.position, target.SpriteRenderer.bounds.ClosestPoint(transform.position)) < Game.Data.AttackDistance;
    }

    private void Health_OnDied()
    {
        SetState(EState.None);
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
