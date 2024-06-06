using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUnlock : MonoBehaviour
{
    public int currentStageIndex; // 현재 스테이지 인덱스

    void Update()
    {
        // 스페이스바를 눌렀을 때 스테이지 클리어 처리
        if (GameManager.Instance.isGoal == true)
        {
            OnStageCleared();
        }
    }

    public void OnStageCleared()
    {
        // 현재 스테이지를 클리어 상태로 설정
        PlayerPrefs.SetInt("Stage" + currentStageIndex.ToString() + "Unlocked", 1);

        // 다음 스테이지 해금
        int nextStageIndex = currentStageIndex + 1;
        PlayerPrefs.SetInt("Stage" + nextStageIndex.ToString() + "Unlocked", 1);

        // PlayerPrefs 저장
        PlayerPrefs.Save();

        Debug.Log("Stage " + currentStageIndex + " cleared. Stage " + nextStageIndex + " unlocked.");

        GameManager.Instance.isGoal = false;
    }
}
