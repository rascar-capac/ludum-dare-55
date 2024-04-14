using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<Game>();
            }

            return _instance;
        }
    }
    public static GameData Data => Instance._data;
    public static EnvironmentHolder Environment => Instance._environment;

    [SerializeField] private GameData _data;
    [SerializeField] private EnvironmentHolder _environment;

    private static Game _instance;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
    }
}
