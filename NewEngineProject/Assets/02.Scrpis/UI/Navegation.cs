using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Navegation : MonoBehaviour
{
    public Transform Destination; // 목적지 Transform
    public Slider progressSlider; // 슬라이더 UI
    public float maxDistance = 2000f; // 최대 거리
    private Transform playerTransform;
    private NavMeshPath navMeshPath;

    void Start()
    {
        navMeshPath = new NavMeshPath();
    }

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
        // 네브메쉬 경로 계산
        NavMesh.CalculatePath(playerTransform.position, Destination.position, NavMesh.AllAreas, navMeshPath);
        float distance = CalculatePathDistance(navMeshPath); // 네브메쉬 경로 거리 계산
        float progress = 1 - Mathf.Clamp01(distance / maxDistance); // 슬라이더 값 계산 (0 ~ 1 사이)
        progressSlider.value = progress; // 슬라이더 값 업데이트
    }

    float CalculatePathDistance(NavMeshPath path)
    {
        float distance = 0f;

        if (path.corners.Length < 2)
        {
            return distance;
        }

        for (int i = 1; i < path.corners.Length; i++)
        {
            distance += Vector3.Distance(path.corners[i - 1], path.corners[i]);
        }

        return distance;
    }
}
