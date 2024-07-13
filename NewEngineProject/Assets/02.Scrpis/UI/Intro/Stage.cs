using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoSingleton<Stage>
{
    public string sceneName; // �̵��� �� �̸�
    public int stageIndex; // �������� �ε���
    private Button button;
    private Image buttonImage;

    public Image unlockedImage; // �رݵ� �������� �̹���
    public Image lockedImage; // ��� �������� �̹���

    protected override void Awake()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        // ���������� �رݵǾ� �ִ��� Ȯ��
        if (IsStageUnlocked())
        {
            button.onClick.AddListener(() => UIManager.Instance.OnStageButtonClicked(sceneName));
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
        bool isUnlocked = PlayerPrefs.GetInt("Stage" + stageIndex.ToString() + "Unlocked", 0) == 1;
        return isUnlocked;
    }
}
