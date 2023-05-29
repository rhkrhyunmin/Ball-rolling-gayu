using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallTimer : MonoBehaviour
{
    public float timer = 0;
    public TextMeshProUGUI TimerTMP;

    private void Awake()
    {
        TimerTMP = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        Timer();
    }

    public void Timer()
    {
        timer += Time.deltaTime;
        TimerTMP.text = timer.ToString("F2");
    }

}
