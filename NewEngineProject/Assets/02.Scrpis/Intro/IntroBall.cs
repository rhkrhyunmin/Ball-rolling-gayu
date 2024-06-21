using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class IntroBall : MonoBehaviour
{
    public float speed = 5f; // 공의 이동 속도
    public float checkInterval = 1f; // 위치를 체크하는 간격

    private List<Vector3> positions = new List<Vector3>();

    void Start()
    {
        StartCoroutine(MoveBall());
    }

    IEnumerator MoveBall()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(startPosition.x + 10, startPosition.y, startPosition.z); // 오른쪽으로 10 유닛 이동

        float distance = Vector3.Distance(startPosition, endPosition);
        float elapsedTime = 0;

        while (elapsedTime < distance / speed)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / (distance / speed));
            elapsedTime += Time.deltaTime;

            // 위치 체크 및 저장
            if (elapsedTime % checkInterval < Time.deltaTime)
            {
                positions.Add(transform.position);
            }

            yield return null;
        }

        // 마지막 위치 체크
        positions.Add(endPosition);
    }

}
