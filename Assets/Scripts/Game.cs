using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance {get; private set;}
    public static GameData Data => Instance._data;

    [SerializeField] private GameData _data;

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
