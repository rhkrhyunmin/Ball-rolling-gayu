using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public string sceneName; // �̵��� �� �̸�
    public int stageIndex; // �������� �ε���
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        // ���������� �رݵǾ� �ִ��� Ȯ��
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
        // ���������� �̵�
        SceneManager.LoadScene(sceneName);
    }
}
