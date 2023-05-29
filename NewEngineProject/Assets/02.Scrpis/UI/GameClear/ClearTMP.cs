using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTMP : MonoBehaviour
{
    public float speed = 1f; // 글자의 이동 속도 설정
    private Vector3 startPosition;
    private bool isMoving = true;

    void Start()
    {
        startPosition = transform.position;
        Invoke("StopMovement", 3f); // 3초 후에 StopMovement 함수 호출
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
