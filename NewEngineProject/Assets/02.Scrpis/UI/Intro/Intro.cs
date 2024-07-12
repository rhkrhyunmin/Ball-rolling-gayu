using System.Collections;
using System.Collections.Generic;
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
        BlinkText();

        // �г� �ִϸ��̼� �ʱ�ȭ
        offScreenPosition = new Vector3(Screen.width, panel.localPosition.y, panel.localPosition.z);
        ResetPanelPosition();
    }

    protected override void OnDestroy()
    {
        // �г� ��ġ�� �ʱ�ȭ
        ResetPanelPosition();
    }

    void BlinkText()
    {
        _startText.DOFade(0, 1.5f).SetLoops(-1, LoopType.Yoyo); // �ؽ�Ʈ ������ ȿ��
    }

    public void OpenPanel()
    {
        stageUI.SetActive(true);
        panel.DOKill(); // ������ �ִϸ��̼��� ����
        panel.DOLocalMove(Vector3.zero, animationDuration).SetEase(Ease.OutBack); // �г��� ȭ�� �߾����� �̵�
        Debug.Log("1");
    }

    public void ClosePanel()
    {
        panel.DOKill(); // ������ �ִϸ��̼��� ����
        panel.DOLocalMove(offScreenPosition, animationDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            stageUI.SetActive(false);
        }); // �г��� ȭ�� ������ �̵�
    }

    public void NextScene()
    {
        OpenPanel();
    }

    public void Back()
    {
        ClosePanel();
    }

    private void ResetPanelPosition()
    {
        panel.localPosition = offScreenPosition;
    }
}
