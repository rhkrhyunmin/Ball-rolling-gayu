using HeathenEngineering.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public void Update()
    {
        StartCoroutine(Shake(3, 2));
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f,1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x,y,originalPos.z);

            elapsed += Time.deltaTime;
            Debug.Log("3");

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
