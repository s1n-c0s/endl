using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
 
    public PlayerData PlayerData;
    public EnemyData EnemyData;

    private void Awake()
    {
        // Implementing the Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
           /* Destroy(gameObject);*/
            return;
        }

        /*DontDestroyOnLoad(gameObject);*/
    }
}
