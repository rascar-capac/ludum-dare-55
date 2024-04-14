public abstract class AMinionState
{
    protected Minion _minion;

    public abstract void Update();
    public virtual void UpdateTarget() { }
    public virtual void DrawGizmos() { }

    public AMinionState(Minion minion)
    {
        _minion = minion;
    }
}
