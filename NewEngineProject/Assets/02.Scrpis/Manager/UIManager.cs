using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("GameUI")]
    public GameObject pauseUI;
    public GameObject settingUI;
    public GameObject playerStatUI;
    public GameObject victoryUI;
    [Header("Speed")]
    public Image speedSlider;
    //public Image speedImage;
    [Header("GameOverUI")]
    public GameObject gameOverUI;
    

    private void Awake()
    {
        // 싱글톤 인스턴스를 설정합니다.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 변경되어도 파괴되지 않도록 설정합니다.
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 새로 생성된 객체를 파괴합니다.
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUI();
            Time.timeScale = 0;
        }
    }

    public void SettingUI()
    {
        pauseUI.SetActive(false);
        settingUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void ConinueGame()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseUI()
    {
        pauseUI.SetActive(true);
    }

    public void MainScene()
    {
        SceneManager.LoadScene(0);
    }

    public void VictroyUI()
    {
        StartCoroutine(OnvictoryUI(3f));
    }
    
    public IEnumerator OnvictoryUI(float duration)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        playerStatUI.SetActive(false);
        victoryUI.SetActive(true);

        yield return new WaitForSeconds(duration);

        GameManager.Instance.isGoal = true;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
