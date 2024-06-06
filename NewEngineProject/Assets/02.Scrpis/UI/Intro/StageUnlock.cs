using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUnlock : MonoBehaviour
{
    public int currentStageIndex; // ���� �������� �ε���

    void Update()
    {
        // �����̽��ٸ� ������ �� �������� Ŭ���� ó��
        if (GameManager.Instance.isGoal == true)
        {
            OnStageCleared();
        }
    }

    public void OnStageCleared()
    {
        // ���� ���������� Ŭ���� ���·� ����
        PlayerPrefs.SetInt("Stage" + currentStageIndex.ToString() + "Unlocked", 1);

        // ���� �������� �ر�
        int nextStageIndex = currentStageIndex + 1;
        PlayerPrefs.SetInt("Stage" + nextStageIndex.ToString() + "Unlocked", 1);

        // PlayerPrefs ����
        PlayerPrefs.Save();

        Debug.Log("Stage " + currentStageIndex + " cleared. Stage " + nextStageIndex + " unlocked.");

        GameManager.Instance.isGoal = false;
    }
}
