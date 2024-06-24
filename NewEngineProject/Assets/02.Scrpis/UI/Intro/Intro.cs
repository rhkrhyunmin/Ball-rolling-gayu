using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class Intro : MonoBehaviour
{
    public GameObject stageUI;
    public RectTransform panel;
    public TextMeshProUGUI _startText;
    public float animationDuration = 0.5f;
    private Vector3 offScreenPosition;

    private void Start()
    {
        // 텍스트 깜빡임 효과 시작
        BlinkText();

        // 패널 애니메이션 초기화
        offScreenPosition = new Vector3(Screen.width, panel.localPosition.y, panel.localPosition.z);
        panel.localPosition = offScreenPosition;
    }

    void BlinkText()
    {
        _startText.DOFade(0, 1.5f).SetLoops(-1, LoopType.Yoyo); // 텍스트 깜빡임 효과
    }

    public void OpenPanel()
    {
        stageUI.SetActive(true);
        panel.DOLocalMove(Vector3.zero, animationDuration).SetEase(Ease.OutBack); // 패널을 화면 중앙으로 이동
    }

    public void ClosePanel()
    {
        panel.DOLocalMove(offScreenPosition, animationDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            stageUI.SetActive(false);
        }); // 패널을 화면 밖으로 이동
    }

    public void NextScene()
    {
        OpenPanel();
        //LoadingScene.LoadScene("Tutorial");
    }

    public void Back()
    {
        ClosePanel();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
