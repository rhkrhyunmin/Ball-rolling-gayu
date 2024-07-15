using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public GameObject player;

    public float timer;
    public bool isGoal = false;
    public bool isShrinking = false;

    void Update()
    {
        timer += Time.deltaTime;
        // "ball" �±׸� ���� ���� ������Ʈ�� ã���ϴ�.
        GameObject ballObject = GameObject.FindGameObjectWithTag("Ball");
        if (ballObject != null)
        {
            // ballObject�� null�� �ƴ϶�� �ش� ���� ������Ʈ�� player ������ �Ҵ��մϴ�.
            player = ballObject;
        }
    }

}
