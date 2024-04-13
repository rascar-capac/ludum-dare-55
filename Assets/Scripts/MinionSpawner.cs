using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    public bool CanSpawn => Time.time > _nextAvailableTime;

    [SerializeField] private Minion _minionPrefab;
    [SerializeField] private MinionsManager _minionsManager;
    [SerializeField] private Transform _origin;
    [SerializeField] private SpriteRenderer _button;
    [SerializeField] private SpriteRenderer _fist;

    private Vector2 _fistUpPosition;
    private float _nextAvailableTime;

    public void SpawnMinion(int count)
    {
        if(!CanSpawn)
        {
            return;
        }

        for(int minionIndex = 0; minionIndex < count; minionIndex++)
        {
            var position = GetSpawningPosition();
            var minion = Instantiate(_minionPrefab, position, Quaternion.identity);
            //TODO: random flip
            _minionsManager.AddMinion(minion);
        }

        _nextAvailableTime = Time.time + Game.Data.SpawningCooldown;
    }

    private Vector2 GetSpawningPosition()
    {
        //TODO: return position on circle near the closest enemy
        float randomAngleRad = Random.Range(0, 2 * Mathf.PI);
        float radius = Game.Data.SpawningCircleRadius;

        return new(
            _origin.position.x + radius * Mathf.Cos(randomAngleRad),
            _origin.position.y + radius * Mathf.Sin(randomAngleRad)
            );
    }

    private void UpdateCooldownFeedbacks()
    {
        if(CanSpawn)
        {
            return;
        }

        _button.color = Game.Data.ButtonGradient.Evaluate((Time.time - _nextAvailableTime + Game.Data.SpawningCooldown) / Game.Data.SpawningCooldown);
        //TODO: add spread effect when ready
    }

    private void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnMinion(1);
        }

        UpdateFistPosition();
    }

    private void UpdateFistPosition()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _fist.transform.position = _button.transform.position;
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            _fist.transform.position = _fistUpPosition;
        }
    }

    [ContextMenu("Spawn minion")]
    private void SpawnMinion()
    {
        if(Application.isPlaying)
        {
            SpawnMinion(1);
        }
    }

    private void Awake()
    {
        _fistUpPosition = _fist.transform.position;
        UpdateFistPosition();
    }

    private void Update()
    {
        HandleInput();
        UpdateCooldownFeedbacks();
    }
}
