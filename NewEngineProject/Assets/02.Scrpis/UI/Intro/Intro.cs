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

        // 패널 애니메이션 초기화
        offScreenPosition = new Vector3(Screen.width, panel.localPosition.y, panel.localPosition.z);
        ResetPanelPosition();
    }

    protected override void OnDestroy()
    {
        // 패널 위치를 초기화
        ResetPanelPosition();
    }

    void BlinkText()
    {
        _startText.DOFade(0, 1.5f).SetLoops(-1, LoopType.Yoyo); // 텍스트 깜빡임 효과
    }

    public void OpenPanel()
    {
        stageUI.SetActive(true);
        panel.DOKill(); // 기존의 애니메이션을 중지
        panel.DOLocalMove(Vector3.zero, animationDuration).SetEase(Ease.OutBack); // 패널을 화면 중앙으로 이동
        Debug.Log("1");
    }

    public void ClosePanel()
    {
        panel.DOKill(); // 기존의 애니메이션을 중지
        panel.DOLocalMove(offScreenPosition, animationDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            stageUI.SetActive(false);
        }); // 패널을 화면 밖으로 이동
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
