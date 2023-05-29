using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTMP : MonoBehaviour
{
    public float speed = 1f; // ������ �̵� �ӵ� ����
    private Vector3 startPosition;
    private bool isMoving = true;

    void Start()
    {
        startPosition = transform.position;
        Invoke("StopMovement", 3f); // 3�� �Ŀ� StopMovement �Լ� ȣ��
    }

    void Update()
    {
        if (isMoving)
        {
            float newY = transform.position.y - speed * Time.deltaTime;
            transform.position = new Vector3(startPosition.x, newY, startPosition.z);
        }
    }

    void StopMovement()
    {
        isMoving = false;
    }
}
