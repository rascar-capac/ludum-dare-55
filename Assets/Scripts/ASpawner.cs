using UnityEngine;
using UnityEngine.Events;

public abstract class ASpawner : MonoBehaviour
{
    public bool CanSpawn => Time.time > _nextAvailableTime;

    [SerializeField] protected Minion _minionPrefab;
    [SerializeField] protected MinionsManager _minionsManager;

    protected float _nextAvailableTime;

    public UnityEvent<Minion> OnMinionSpawned {get;} = new();

    public abstract void SpawnMinions();
    protected abstract Vector2 GetSpawningPosition();
    protected abstract float GetNextAvailableSpawningTime();

    protected bool SpawnMinions(int count)
    {
        if(!CanSpawn)
        {
            return false;
        }

        for(int minionIndex = 0; minionIndex < count; minionIndex++)
        {
            SpawnMinion();
        }

        _nextAvailableTime = GetNextAvailableSpawningTime();

        return true;
    }

    [ContextMenu("Spawn minion")]
    protected void SpawnMinion()
    {
        var position = GetSpawningPosition();
        var minion = Instantiate(_minionPrefab, position, Quaternion.identity);
        //TODO: random flip
        OnMinionSpawned.Invoke(minion);
    }
}
