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
        Time.timeScale = 1;
        offScreenPosition = new Vector3(Screen.width + 70, panel.localPosition.y, panel.localPosition.z);
    }

    public void OpenPanel()
    {
        panel.DOLocalMove(Vector3.zero, animationDuration).SetEase(Ease.OutBack); // �г��� ȭ�� �߾����� �̵�
    }

    public void ClosePanel()
    {
        panel.DOLocalMove(offScreenPosition, animationDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            Debug.Log(offScreenPosition);
        });
    }
}