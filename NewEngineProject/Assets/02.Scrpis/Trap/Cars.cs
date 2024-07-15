using System.Collections;
using UnityEngine;

public class Cars : MonoBehaviour
{
    public Vector3 StartPos;
    public Vector3 EndPos;
    public TrapSo carsSo;
    public LayerMask carLayer; // 감지할 레이어

    void Start()
    {
        // 초기 위치를 월드 좌표계에서 설정
        transform.position = transform.parent.TransformPoint(StartPos);
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (true)
        {
            Debug.Log("Moving to End Position: " + EndPos);
            // 목표 위치로 이동
            yield return StartCoroutine(MoveToPosition(transform.parent.TransformPoint(EndPos)));
            Debug.Log("Reached End Position");

            Debug.Log("Resetting to Start Position: " + StartPos);
            transform.position = transform.parent.TransformPoint(StartPos);
        }
    }

    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            // 앞에 장애물이 있는지 레이캐스트로 확인
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2, carLayer))
            {
                Vector3 backOffPosition = transform.position - transform.forward * 2;
                transform.position = Vector3.MoveTowards(transform.position, backOffPosition, carsSo.MovementSpeed * Time.deltaTime);
                yield return new WaitForSeconds(0.5f); // 잠시 대기
                continue; // 다시 충돌 검사
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, carsSo.MovementSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
    }
}
