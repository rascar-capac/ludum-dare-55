using System;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public AMinionState CurrentState {get; private set;}

    public void Initialize()
    {
        SetState(EState.Wandering);
    }

    public void SetState(EState stateType)
    {
        CurrentState = stateType switch
        {
            EState.None => null,
            EState.Wandering => new MinionWanderingState(this),
            _ => throw new NotImplementedException()
        };
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
    }
}
