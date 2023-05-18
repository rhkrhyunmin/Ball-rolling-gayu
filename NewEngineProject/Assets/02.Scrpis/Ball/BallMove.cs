using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private bool canMove = true; // �̵� �������� ���θ� ��Ÿ���� ����

    private void Update()
    {
        if (canMove)
        {
            // ����Ű �Է� ó��
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // ������ ó��
            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
            transform.Translate(movement * speed * Time.deltaTime);
        }
    }

    private IEnumerator FireSequence()
    {
        // ���� �߻� ������ ����

        // ���� ���� ���� ������ ���
        yield return new WaitForSeconds(1f); // ���ϴ� ��� �ð�

        canMove = true; // �̵� ���
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // ���� ����� ���� ó��
            canMove = true; // �̵� ���
        }
    }
}
