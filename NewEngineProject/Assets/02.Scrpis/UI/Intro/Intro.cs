using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class Intro : MonoSingleton<Intro>
{
    public GameObject stageUI;
    public RectTransform panel;
    public TextMeshProUGUI _startText;
    public float animationDuration = 0.5f;
    private Vector3 offScreenPosition;

    protected override void Awake()
    {
        base.Awake();

        // �г� �ִϸ��̼� �ʱ�ȭ
        offScreenPosition = new Vector3(Screen.width + 70, panel.localPosition.y, panel.localPosition.z);
        ResetPanelPosition(); // �г� ��ġ�� �ʱ�ȭ
    }

    public void OpenPanel()
    {
        panel.DOLocalMove(Vector3.zero, animationDuration).SetEase(Ease.OutBack); // �г��� ȭ�� �߾����� �̵�
    }

    public void ClosePanel()
    {
        panel.DOLocalMove(offScreenPosition, animationDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            // �ʿ��� �۾��� OnComplete�� �߰��� �� �ֽ��ϴ�.
        });
    }

    public void NextScene()
    {
        // �г��� �ݰ� ���� ������ �̵�
        ClosePanel();
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        // ���� �񵿱������� �ε�
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("NextSceneName"); // ���� �� �̸����� �����ϼ���.

        // �� �ε尡 �Ϸ�� ������ ���
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // �� �ε� �Ϸ� �� �г� �ʱ�ȭ �� �ִϸ��̼� ����
        OnSceneLoaded();
    }

    private void OnSceneLoaded()
    {
        ResetPanelPosition();
        OpenPanel();
    }

    private void ResetPanelPosition()
    {
        panel.localPosition = offScreenPosition;
    }
}
