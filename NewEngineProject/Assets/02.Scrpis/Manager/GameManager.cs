using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public bool isBoss = false;
    public bool isGoal = false;

    private static GameManager instance;

    void Update()
    {
        // "ball" 태그를 가진 게임 오브젝트를 찾습니다.
        GameObject ballObject = GameObject.FindGameObjectWithTag("Ball");
        if (ballObject != null)
        {
            // ballObject가 null이 아니라면 해당 게임 오브젝트를 player 변수에 할당합니다.
            player = ballObject;
        }
    }

    // 싱글톤 인스턴스를 만듭니다.
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
