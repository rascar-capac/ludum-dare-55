using System.Collections.Generic;
using UnityEngine;

public class MinionsManager : MonoBehaviour
{
    private List<Minion> _minions = new();

    public void AddMinion(Minion minion)
    {
        _minions.Add(minion);
        minion.transform.parent = transform;
    }

    private void Awake()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
