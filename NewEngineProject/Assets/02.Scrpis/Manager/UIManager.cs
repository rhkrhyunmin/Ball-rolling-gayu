using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject Navagation;

    [Header("GameUI")]
    public GameObject pauseUI;
    public GameObject settingUI;
    [Header("Boss")]
    public GameObject bossWarningImage;
    public TextMeshProUGUI TextBossHp;
    public Slider BossHp;
    [Header("Speed")]
    public Slider speedSlider;
    public Image speedImage;
    [Header("Hp")]
    public Slider hpSlider;
    public Image hpImage;
    [Header("GameOverUI")]
    public GameObject gameOverUI;
    

    private void Awake()
    {
        // �̱��� �ν��Ͻ��� �����մϴ�.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� ����Ǿ �ı����� �ʵ��� �����մϴ�.
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �����ϸ� ���� ������ ��ü�� �ı��մϴ�.
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
    
}
