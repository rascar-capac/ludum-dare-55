using System;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public AMinionState CurrentState {get; private set;}

    private SpriteRenderer _platform;
    private SpriteRenderer _button;

    public void Initialize(SpriteRenderer platform, SpriteRenderer button)
    {
        _platform = platform;
        _button = button;
        SetState(EState.Wandering);
    }

    public void SetState(EState stateType)
    {
        CurrentState = stateType switch
        {
            EState.Wandering => new MinionWanderingState(this, _platform.bounds, _button.bounds),
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
        Wandering = 0,
    }
}
