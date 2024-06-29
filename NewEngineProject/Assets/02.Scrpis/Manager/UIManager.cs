using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("GameUI")]
    public GameObject pauseUI;
    public GameObject settingUI;
    public GameObject victoryUI;

    [Header("GameOverUI")]
    public GameObject gameOverUI;

    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    protected override void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        // base.OnDestroy(); 이 호출을 제거합니다.
    }

    private void Start()
    {
        InitializeUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUI();
            Time.timeScale = 0;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        pauseUI = pauseUI ?? GameObject.Find("PauseUI");
        settingUI = settingUI ?? GameObject.Find("SettingUI");
        victoryUI = victoryUI ?? GameObject.Find("VictoryUI");
        gameOverUI = gameOverUI ?? GameObject.Find("GameOverUI");
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

    private IEnumerator OnvictoryUI(float duration)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
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
