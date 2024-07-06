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
    public virtual void ActiveUI(GameObject OnObj)
    {
        // ��� UI ������Ʈ�� ��Ȱ��ȭ
        foreach (GameObject obj in UIObjects)
        {
           obj.SetActive(false);
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



    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void Continue()
    {
        ActiveUI(null); // No UI should be active
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