using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearUI : MonoBehaviour
{
    private float playTime = 0f;
    private bool isPlaying = false;

    private void Start()
    {
        isPlaying = true;
    }

    private void Update()
    {
        if (isPlaying)
        {
            playTime += Time.deltaTime;
        }
    }

    public float GetPlayTime()
    {
        return playTime;
    }

    public void StopPlayTime()
    {
        isPlaying = false;
    }
}
