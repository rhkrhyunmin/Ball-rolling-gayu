using System.Collections;
using UnityEngine;

public class Cars : MonoBehaviour
{
    public Vector3 StartPos;
    public Vector3 EndPos;
    public TrapSo carsSo;
    public LayerMask carLayer; // ������ ���̾�

    void Start()
    {
        // �ʱ� ��ġ�� ���� ��ǥ�迡�� ����
        transform.position = transform.parent.TransformPoint(StartPos);
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (true)
        {
            Debug.Log("Moving to End Position: " + EndPos);
            // ��ǥ ��ġ�� �̵�
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
            // �տ� ��ֹ��� �ִ��� ����ĳ��Ʈ�� Ȯ��
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2, carLayer))
            {
                Vector3 backOffPosition = transform.position - transform.forward * 2;
                transform.position = Vector3.MoveTowards(transform.position, backOffPosition, carsSo.MovementSpeed * Time.deltaTime);
                yield return new WaitForSeconds(0.5f); // ��� ���
                continue; // �ٽ� �浹 �˻�
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, carsSo.MovementSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
    }
}
