using UnityEngine;
using System.Collections;

public class Trail : MonoBehaviour
{
    public Transform startPoint; // 시작 지점
    public Transform endPoint;   // 목표 지점
    public float speed = 7f;   // 이동 속도

    private bool movingToEnd = true; // 목표 지점으로 이동 중인지 여부

    void Start()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (true)
        {
            if (movingToEnd)
            {
                // 목표 지점으로 이동
                yield return StartCoroutine(MoveToPosition(endPoint.position));
                movingToEnd = false;
                // 목표 지점에 도달 후 0도 회전 (상대적인 회전이 아님)
                transform.rotation = Quaternion.Euler(0,-90,0);
            }
            else
            {
                yield return StartCoroutine(MoveToPosition(startPoint.position));
                movingToEnd = true;
                // 목표 지점에 도달 후 180도 회전 (상대적인 회전이 아님)
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
        }
    }

    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition; // 목표 위치에 정확히 설정
    }
}
