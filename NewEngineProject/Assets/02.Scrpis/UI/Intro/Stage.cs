using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public string sceneName; // 이동할 씬 이름
    public int stageIndex; // 스테이지 인덱스
    private Button button;
    private Image buttonImage;

    public Image unlockedImage; // 해금된 스테이지 이미지
    public Image lockedImage; // 잠긴 스테이지 이미지

    private void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        // 스테이지가 해금되어 있는지 확인
        if (IsStageUnlocked())
        {
            Time.timeScale = 1;
            button.onClick.AddListener(() => UIManager.Instance.OnStageButtonClicked(sceneName));
            unlockedImage.gameObject.SetActive(true);
            lockedImage.gameObject.SetActive(false);
        }
        else
        {
            button.interactable = false;
            unlockedImage.gameObject.SetActive(false);
            lockedImage.gameObject.SetActive(true);
            
        }
    }

    bool IsStageUnlocked()
    { 
        if (stageIndex == 0)
        {
            return true;
        }

        bool isUnlocked = PlayerPrefs.GetInt("Stage" + stageIndex.ToString() + "Unlocked", 0) == 1;
        return isUnlocked;
    }
}
