using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
    public Transform destination; // 목적지 위치
    public GameObject indicatorPrefab; // 목적지 아이콘 프리팹
    public float amplitude = 0.5f; // 아이콘의 위아래 움직임 강도
    public float speed = 1.0f; // 아이콘의 움직임 속도
    public float initialYOffset = 30.0f; // 초기 y값 오프셋

    private GameObject indicatorInstance; // 생성된 아이콘 인스턴스
    private float startY; // 아이콘의 초기 y 위치

    void Start()
    {
        // 목적지 아이콘 생성
        indicatorInstance = Instantiate(indicatorPrefab, Vector3.zero, Quaternion.identity);
        startY = indicatorInstance.transform.position.y + initialYOffset; // 초기 y 위치 설정

    }

    void Update()
    {
        // 목적지 아이콘의 위치를 목적지 위치로 이동
        indicatorInstance.transform.position = destination.position;

        // 아이콘의 y 위치를 위아래로 변화시켜 움직임 효과를 줌
        float newY = startY + Mathf.Sin(Time.time * speed) * amplitude;
        indicatorInstance.transform.position = new Vector3(indicatorInstance.transform.position.x, newY, indicatorInstance.transform.position.z);

        // 화살 오브젝트 회전
        indicatorInstance.transform.rotation = Quaternion.Euler(0f, -90f, 90f);
    }
}
