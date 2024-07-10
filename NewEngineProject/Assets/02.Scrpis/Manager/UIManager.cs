using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    public List<GameObject> UIObjects;
    public Image speedGage;
    public Image boostPack;

    [Header("loading")]
    public GameObject LoadingUI;
    public TextMeshProUGUI loadingText;
    public Slider loadingSlider;

    protected override void Awake()
    {
        base.Awake();
        InitializeUI();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    protected override void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #region UI껐다 키기
    public virtual void ActiveUI(GameObject OnObj)
    {
        // 모든 UI 오브젝트를 비활성화
        foreach (GameObject obj in UIObjects)
        {
           obj.SetActive(false);
        }

        // 특정 UI 오브젝트만 활성화
        if (OnObj != null)
        {
            OnObj.SetActive(true);
        }
    }

    public void SettingUI()
    {
        ActiveUI(UIObjects.Find(obj => obj.name == "SettingUI"));
        
    }

    public void PauseUI()
    {
        ActiveUI(UIObjects.Find(obj => obj.name == "PauseUI"));
    }

    public void VictoryUI()
    {
        StartCoroutine(OnVictoryUI(3f));
    }

    private IEnumerator OnVictoryUI(float duration)
    {
        ActiveUI(UIObjects.Find(obj => obj.name == "VictoryUI"));
        yield return new WaitForSeconds(duration);
        GameManager.Instance.isGoal = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    #endregion

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
        // Initialize UIObjects if not set in the inspector
        foreach (var objName in new[] { "PauseUI", "SettingUI", "VictoryUI", "GameOverUI" })
        {
            var obj = GameObject.Find(objName);
            if (obj != null && !UIObjects.Contains(obj))
            {
                UIObjects.Add(obj);
            }
        }
    }

    public void OnStageButtonClicked(string sceneName)
    {
        // 중복 로딩 방지
        if (!LoadingUI.activeSelf)
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }
    }

    private IEnumerator LoadSceneAsync(string levelName)
    {
        LoadingUI.SetActive(true);

        AsyncOperation op = SceneManager.LoadSceneAsync(levelName);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            if (op.progress < 0.9f)
            {
                float progress = Mathf.Clamp01(op.progress / 0.9f);
                loadingSlider.value = progress;
                Debug.Log($"Loading progress: {progress * 100f}%");
                loadingText.text = (progress * 100f).ToString("F2") + "%";
            }
            else
            {
                loadingSlider.value = 1f;
                loadingText.text = "100%";
                Debug.Log("Loading complete: 100%");

                yield return new WaitForSeconds(0.5f);
                op.allowSceneActivation = true;
            }

            yield return null;
        }

        LoadingUI.SetActive(false);
    }



    public void RestartGame(string levelName)
    {
        OnStageButtonClicked(levelName);
        Time.timeScale = 1f;
    }

    public void Continue()
    {
        ActiveUI(null); // No UI should be active
        Time.timeScale = 1f;
    }

    public void MainScene()
    {
        OnStageButtonClicked("Intro");
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}