using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public string sceneName; // 이동할 씬 이름
    public int stageIndex; // 스테이지 인덱스
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        // 스테이지가 해금되어 있는지 확인
        if (IsStageUnlocked())
        {
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            button.interactable = false;
        }
    }

    public bool IsStageUnlocked()
    {
        return PlayerPrefs.GetInt("Stage" + stageIndex.ToString() + "Unlocked", 0) == 1;
    }

    public void OnButtonClick()
    {
        // 스테이지로 이동
        SceneManager.LoadScene(sceneName);
    }
}
