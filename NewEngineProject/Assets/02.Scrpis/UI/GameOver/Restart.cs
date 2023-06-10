using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private void Update()
    {
        // ESC 키를 누르면 게임을 다시 시작합니다.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        // 현재 씬을 다시 로드합니다.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
