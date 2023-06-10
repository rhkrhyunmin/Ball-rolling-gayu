using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private void Update()
    {
        // ESC Ű�� ������ ������ �ٽ� �����մϴ�.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        // ���� ���� �ٽ� �ε��մϴ�.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
