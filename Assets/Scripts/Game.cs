using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance {get; private set;}
    public static GameData Data => Instance._data;
    public static EnvironmentHolder Environment => Instance._environment;

    [SerializeField] private GameData _data;
    [SerializeField] private EnvironmentHolder _environment;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
