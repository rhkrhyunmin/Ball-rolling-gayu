using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Navegation : MonoBehaviour
{
    public Transform Destination; // ������ Transform
    public Slider progressSlider; // �����̴� UI
    public float maxDistance = 2000f; // �ִ� �Ÿ�
    private Transform playerTransform;
    private NavMeshPath navMeshPath;

    void Start()
    {
        navMeshPath = new NavMeshPath();
    }

    void Update()
    {
        // �÷��̾� ��ü�� �������� ã��
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
        // �׺�޽� ��� ���
        NavMesh.CalculatePath(playerTransform.position, Destination.position, NavMesh.AllAreas, navMeshPath);
        float distance = CalculatePathDistance(navMeshPath); // �׺�޽� ��� �Ÿ� ���
        float progress = 1 - Mathf.Clamp01(distance / maxDistance); // �����̴� �� ��� (0 ~ 1 ����)
        progressSlider.value = progress; // �����̴� �� ������Ʈ
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
