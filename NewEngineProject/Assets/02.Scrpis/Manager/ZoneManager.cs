using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    public float speed = 5f;              // 자기장이 이동하는 속도
    public Vector3 startPoint;            // 자기장의 시작 지점
    public Vector3 endPoint;              // 자기장의 목표 지점


    void Start()
    {
        transform.position = startPoint;
    }

    void Update()
    {
        if (GameManager.Instance.isShrinking)
        {
            MoveZone();
        }
    }

    void MoveZone()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);

        // 목표 지점에 도달하면 이동 중지
        if (transform.position == endPoint)
        {
            GameManager.Instance.isShrinking = false;
        }
    }
}
