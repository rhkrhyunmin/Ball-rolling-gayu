using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeletion : MonoBehaviour
{
    public float deletionRadius = 20f; // ���� �ݰ��� �����մϴ�.

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, deletionRadius);

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Ball")) // �±װ� "Ball"�� ���� �����մϴ�.
                {
                    Destroy(collider.gameObject);
                }
            }
        }
    }
}
