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
        _startText.DOFade(0, 1.5f).SetLoops(-1, LoopType.Yoyo); // 0.5�� ���� ���� ���� 0���� ���� ��, ���� ���� ��(1)�� �ٽ� ����. �̰��� �ݺ�(-1�� ���� �ݺ�)�Ͽ� ������ ȿ�� ����
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
