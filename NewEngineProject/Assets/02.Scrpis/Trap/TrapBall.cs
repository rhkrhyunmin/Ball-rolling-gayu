using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBall : MonoBehaviour
{
    private Vector3 originalScale;   // ���� ũ��

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale; // �ʱ� ũ�� ����
    }

    // �浹 ����
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �浹�� ��ü�� �÷��̾����� Ȯ��
        {
            // ��ü ũ�� ����
            transform.localScale = originalScale * 2.5f;

            // �÷��̾�� ������ �ֱ�
            PlayerHp playerHealth = other.GetComponent<PlayerHp>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(5);
            }
        }
    }
}
