using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    public Button mainScene;
    public Button RestartScene;

    public float animDuartion = 2f;

    private void Start()
    {
        OnTimerText();
        mainScene.interactable = false;
        RestartScene.interactable = false;
        UIManager.Instance.boostBackGround.enabled = false;
        UIManager.Instance.boostPack.enabled = false;
        UIManager.Instance.speedGage.enabled = false;
    }

    private void OnTimerText()
    {
        timerText.text = GameManager.Instance.timer.ToString();

        DOTween.To(() => 0.ToString(), x => timerText.text = x.ToString(), timerText.text, animDuartion).OnUpdate(() =>
        {
            int randomValue = Random.Range(0, 10000);
            timerText.text = randomValue.ToString();
        }).OnComplete(() =>
        {
            timerText.text = GameManager.Instance.timer.ToString();
            mainScene.interactable = true;
            RestartScene.interactable = true;
            Time.timeScale = 0;
        });
    }
}
