using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShoot : MonoBehaviour
{
    public GameObject ballPrefab;
    public float forceMagnitude = 10f; // �⺻ ���� ũ��
    public float maxForceMagnitude = 50f; // �ִ� ���� ũ��

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float force = Mathf.Clamp(forceMagnitude, 0f, maxForceMagnitude);
            Vector3 forceVector = new Vector3(0f, 0f, force);

            GameObject ballInstance = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            Rigidbody ballRb = ballInstance.GetComponent<Rigidbody>();
            ballRb.AddForce(forceVector, ForceMode.Impulse);
        }
    }
}
