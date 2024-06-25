using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBall : MonoBehaviour
{
    private Vector3 originalScale;   // 원래 크기

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale; // 초기 크기 저장
    }
}
