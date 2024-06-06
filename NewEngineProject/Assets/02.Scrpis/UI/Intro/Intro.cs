using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class Intro : MonoBehaviour
{
    public GameObject stageUI;
    public TextMeshProUGUI _startText;
    public void Start()
    {
        BlinkText();
    }

    void BlinkText()
    {
        _startText.DOFade(0, 1.5f).SetLoops(-1, LoopType.Yoyo); // 0.5초 동안 알파 값을 0으로 변경 후, 원래 알파 값(1)로 다시 변경. 이것을 반복(-1은 무한 반복)하여 깜빡임 효과 생성
    }

    public void NextScene()
    {
        stageUI.SetActive(true);
        //LoadingScence.LoadScene("Tutorial");
    }

    public void Back()
    {
        stageUI.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
