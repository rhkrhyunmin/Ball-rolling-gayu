using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Navegation : MonoBehaviour
{
    public Transform Destination; // 목적지 Transform
    public Slider progressSlider; // 슬라이더 UI
    public float maxDistance = 2000f; // 최대 거리
    private Transform playerTransform;

    void Update()
    {
        // 플레이어 객체를 동적으로 찾기
        Player playerObject = FindObjectOfType<Player>();
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }

        if (playerTransform != null)
        {
            UpdateProgress();
            
        }
    }

    void UpdateProgress()
    {
        float distance = Vector3.Distance(playerTransform.position, Destination.position); // 플레이어와 목적지 사이의 거리 계산
        float progress = 1 - Mathf.Clamp01(distance / maxDistance); // 슬라이더 값 계산 (0 ~ 1 사이)
        progressSlider.value = progress; // 슬라이더 값 업데이트
    }
}
