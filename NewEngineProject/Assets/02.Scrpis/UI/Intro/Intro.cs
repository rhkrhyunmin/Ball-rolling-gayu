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
        // �ؽ�Ʈ ������ ȿ�� ����
        BlinkText();

        // �г� �ִϸ��̼� �ʱ�ȭ
        offScreenPosition = new Vector3(Screen.width, panel.localPosition.y, panel.localPosition.z);
        panel.localPosition = offScreenPosition;
    }

    void BlinkText()
    {
        _startText.DOFade(0, 1.5f).SetLoops(-1, LoopType.Yoyo); // �ؽ�Ʈ ������ ȿ��
    }

    public void OpenPanel()
    {
        stageUI.SetActive(true);
        panel.DOLocalMove(Vector3.zero, animationDuration).SetEase(Ease.OutBack); // �г��� ȭ�� �߾����� �̵�
    }

    public void ClosePanel()
    {
        panel.DOLocalMove(offScreenPosition, animationDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            stageUI.SetActive(false);
        }); // �г��� ȭ�� ������ �̵�
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
