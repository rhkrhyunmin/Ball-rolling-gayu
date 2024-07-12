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

    #region UI���� Ű��
    public virtual void ActiveUI(GameObject OnObj, bool keepOthersActive = false)
    {
        if (!keepOthersActive)
        {
            // ��� UI ������Ʈ�� ��Ȱ��ȭ
            foreach (GameObject obj in UIObjects)
            {
                obj.SetActive(false);
            }
        }

        // Ư�� UI ������Ʈ�� Ȱ��ȭ
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
        ActiveUI(UIObjects.Find(obj => obj.name == "VictoryUI"));
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
        // �ߺ� �ε� ����
        if (!LoadingUI.activeSelf)
        {
            ActiveUI(LoadingUI, true); 
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
                loadingText.text = (progress * 100f).ToString("F2") + "%";
            }
            else
            {
                loadingSlider.value = 1f;
                loadingText.text = "100%";
                yield return new WaitForSeconds(0.5f);
                op.allowSceneActivation = true;
            }

            yield return null;
        }

        Debug.Log("LoadSceneAsync completed");
        LoadingUI.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        OnStageButtonClicked(SceneManager.GetActiveScene().name);
    }

    public void Continue()
    {
        ActiveUI(null);
        Time.timeScale = 1f;
    }

    public void MainScene()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
