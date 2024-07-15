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
        // "ball" 태그를 가진 게임 오브젝트를 찾습니다.
        GameObject ballObject = GameObject.FindGameObjectWithTag("Ball");
        if (ballObject != null)
        {
            // ballObject가 null이 아니라면 해당 게임 오브젝트를 player 변수에 할당합니다.
            player = ballObject;
        }
    }

}
