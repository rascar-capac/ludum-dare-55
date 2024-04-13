using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    [SerializeField] private Minion _minionPrefab;
    [SerializeField] private MinionsManager _minionsManager;
    [SerializeField] private Transform _origin;

    public void SpawnMinion(int count)
    {
        for(int minionIndex = 0; minionIndex < count; minionIndex++)
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
        float randomAngleRad = Random.Range(0, 2 * Mathf.PI);
        float radius = GameManager.Instance.GameData.SpawningCircleRadius;

        return new(
            _origin.position.x + radius * Mathf.Cos(randomAngleRad),
            _origin.position.y + radius * Mathf.Sin(randomAngleRad)
            );
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
