using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public bool isBoss = false;
    public bool isGoal = false;

    private static GameManager instance;

    void Update()
    {
        // "ball" �±׸� ���� ���� ������Ʈ�� ã���ϴ�.
        GameObject ballObject = GameObject.FindGameObjectWithTag("Ball");
        if (ballObject != null)
        {
            // ballObject�� null�� �ƴ϶�� �ش� ���� ������Ʈ�� player ������ �Ҵ��մϴ�.
            player = ballObject;
        }
    }

    // �̱��� �ν��Ͻ��� ����ϴ�.
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
