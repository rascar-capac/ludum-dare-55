using System;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public AMinionState CurrentState {get; private set;}

    [SerializeField] private bool _isEnemy;

    private MinionsManager _manager;

    public void Initialize(MinionsManager manager)
    {
        _manager = manager;
        SetState(_isEnemy? EState.AggressiveRunning : EState.AggressiveRunning);
    }

    public void SetState(EState stateType)
    {
        CurrentState = stateType switch
        {
            EState.None => null,
            EState.Wandering => new MinionWanderingState(this),
            EState.AggressiveRunning => new MinionAggressiveRunState(this),
            _ => throw new NotImplementedException()
        };
    }

    public bool TryGetTargetPosition(out Vector2 targetPosition)
    {
        if(_isEnemy)
        {
            //TODO: return nearest minion if in detection area
            targetPosition =  Game.Environment.ButtonBox.center;

            return true;
        }

        bool nearest_minion_found = _manager.TryGetNearestMinion(this, out Minion nearestMinion);
        targetPosition = nearest_minion_found ? nearestMinion.transform.position : Vector2.zero;

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

    public enum EState
    {
        None = -1,
        Wandering = 0,
        AggressiveRunning = 1,
    }
}
