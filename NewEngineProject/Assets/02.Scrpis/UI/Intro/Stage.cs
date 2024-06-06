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
    private Image buttonImage;

    public Sprite unlockedImage; // 해금된 스테이지 이미지
    public Sprite lockedImage; // 잠긴 스테이지 이미지

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        // 스테이지가 해금되어 있는지 확인
        if (IsStageUnlocked())
        {
            button.onClick.AddListener(OnButtonClick);
            buttonImage.sprite = unlockedImage; // 해금된 이미지로 변경
        }
        else
        {
            //button.interactable = false; // 잠긴 스테이지는 버튼 비활성화
            buttonImage.sprite = lockedImage; // 잠긴 이미지로 변경
        }
    }

    bool IsStageUnlocked()
    {
        // PlayerPrefs에서 스테이지 잠금 여부 확인
        return PlayerPrefs.GetInt("Stage" + stageIndex.ToString() + "Unlocked", 0) == 1;
    }

    public void OnButtonClick()
    {
        // 스테이지로 이동
        SceneManager.LoadScene(sceneName);
    }
}
