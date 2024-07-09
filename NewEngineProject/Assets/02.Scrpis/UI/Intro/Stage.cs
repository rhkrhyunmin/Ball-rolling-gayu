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

    public Image unlockedImage; // �رݵ� �������� �̹���
    public Image lockedImage; // ��� �������� �̹���

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        // ���������� �رݵǾ� �ִ��� Ȯ��
        if (IsStageUnlocked())
        {
            button.onClick.AddListener(OnButtonClick);
            unlockedImage.gameObject.SetActive(true);
            lockedImage.gameObject.SetActive(false);
        }
        else
        {
            unlockedImage.gameObject.SetActive(false);
            lockedImage.gameObject.SetActive(true);
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
