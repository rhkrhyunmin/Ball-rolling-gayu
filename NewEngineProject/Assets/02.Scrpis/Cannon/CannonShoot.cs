using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShoot : MonoBehaviour
{
    public Rigidbody ballPrefab;
    public float forceMultiplier = 1f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ������ ����
            Rigidbody ballInstance = Instantiate(ballPrefab, transform.position, Quaternion.identity);

            // ���� ũ�� ���
            float forceMagnitude = CalculateForceMagnitude();

            // �� ����
            Vector3 force = new Vector3(0f, 0f, forceMagnitude);
            ballInstance.AddForce(force);
        }
    }

    private float CalculateForceMagnitude()
    {
        // ���� ũ�⸦ ����ϴ� ������ �߰��մϴ�.
        // ���� ���, Ű�� ������ �ð��� ���� ���� ũ�⸦ ��ȭ��ų �� �ֽ��ϴ�.
        return forceMultiplier;  // ���� ũ�⿡ ���� ���� ��ȯ�մϴ�.
    }
}
