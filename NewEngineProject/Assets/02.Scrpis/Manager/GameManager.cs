using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public bool isBoss = false;

    private static GameManager instance;

    public void Update()
    {
        player = FindObjectOfType<Player>();
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject managerObject = new GameObject("GameManager");
                    instance = managerObject.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
}
