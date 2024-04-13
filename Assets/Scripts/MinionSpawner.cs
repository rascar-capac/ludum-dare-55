using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    [SerializeField] private Minion _minionPrefab;
    [SerializeField] private MinionsManager _minionsManager;

    public void SpawnMinion(int count)
    {
        for(int minion_index = 0; minion_index < count; minion_index++)
        {
            var position = GetSpawningPosition();
            var minion = Instantiate(_minionPrefab, position, Quaternion.identity);
            //TODO: random flip
            _minionsManager.AddMinion(minion);
        }
    }

    private Vector2 GetSpawningPosition()
    {
        //TODO: return position on circle near the closest enemy
        return Vector2.zero;
    }

    [ContextMenu("Spawn minion")]
    private void SpawnMinion()
    {
        if(Application.isPlaying)
        {
            SpawnMinion(1);
        }
    }
}
