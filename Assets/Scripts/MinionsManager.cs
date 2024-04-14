using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MinionsManager : MonoBehaviour
{
    [SerializeField] private MinionsManager _adversaryManager;
    [SerializeField] private ASpawner _spawner;

    private List<Minion> _minions = new();

    public UnityEvent<Minion> OnMinionAdded {get;} = new();

    public void AddMinion(Minion minion)
    {
        _minions.Add(minion);
        minion.transform.parent = transform;
        minion.Initialize(this);

        OnMinionAdded.Invoke(minion);
    }

    public bool TryGetNearestMinion(Minion originMinion, out Minion nearestMinion, List<Minion> subset = null)
    {
        float minDistance = float.MaxValue;
        nearestMinion = null;

        foreach(Minion minion in subset ?? _adversaryManager._minions)
        {
            if(minion == originMinion)
            {
                continue;
            }

            float distance = Vector3.Distance(originMinion.transform.position, minion.transform.position);

            if(distance < minDistance)
            {
                minDistance = distance;
                nearestMinion = minion;
            }
        }

        return nearestMinion != null;
    }

    public bool TryGetNearestMinion(Vector2 origin, out Minion nearestMinion, List<Minion> subset = null)
    {
        float minDistance = float.MaxValue;
        nearestMinion = null;

        foreach(Minion minion in subset ?? _adversaryManager._minions)
        {
            float distance = Vector3.Distance(origin, minion.transform.position);

            if(distance < minDistance)
            {
                minDistance = distance;
                nearestMinion = minion;
            }
        }

        return nearestMinion != null;
    }

    private void Spawner_OnMinionSpawned(Minion minion)
    {
        AddMinion(minion);
    }

    private void AdversaryManager_OnMinionAdded(Minion _)
    {
        foreach(Minion minion in _minions)
        {
            minion.UpdateTarget();
        }
    }

    private void Awake()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        _spawner.OnMinionSpawned.AddListener(Spawner_OnMinionSpawned);
        _adversaryManager.OnMinionAdded.AddListener(AdversaryManager_OnMinionAdded);
    }

    private void OnDestroy()
    {
        _spawner.OnMinionSpawned.RemoveListener(Spawner_OnMinionSpawned);
        _adversaryManager.OnMinionAdded.RemoveListener(AdversaryManager_OnMinionAdded);
    }
}
