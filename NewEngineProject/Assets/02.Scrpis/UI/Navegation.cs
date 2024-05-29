using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Navegation : MonoBehaviour
{
    public Transform Destination; // ������ Transform
    public Slider progressSlider; // �����̴� UI
    public float maxDistance = 2000f; // �ִ� �Ÿ�
    private Transform playerTransform;

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
        float distance = Vector3.Distance(playerTransform.position, Destination.position); // �÷��̾�� ������ ������ �Ÿ� ���
        float progress = 1 - Mathf.Clamp01(distance / maxDistance); // �����̴� �� ��� (0 ~ 1 ����)
        progressSlider.value = progress; // �����̴� �� ������Ʈ
    }
}
