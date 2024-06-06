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
    private Image buttonImage;

    public Sprite unlockedImage; // �رݵ� �������� �̹���
    public Sprite lockedImage; // ��� �������� �̹���

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        // ���������� �رݵǾ� �ִ��� Ȯ��
        if (IsStageUnlocked())
        {
            button.onClick.AddListener(OnButtonClick);
            buttonImage.sprite = unlockedImage; // �رݵ� �̹����� ����
        }
        else
        {
            //button.interactable = false; // ��� ���������� ��ư ��Ȱ��ȭ
            buttonImage.sprite = lockedImage; // ��� �̹����� ����
        }
    }

    bool IsStageUnlocked()
    {
        // PlayerPrefs���� �������� ��� ���� Ȯ��
        return PlayerPrefs.GetInt("Stage" + stageIndex.ToString() + "Unlocked", 0) == 1;
    }

    public void OnButtonClick()
    {
        // ���������� �̵�
        SceneManager.LoadScene(sceneName);
    }
}
