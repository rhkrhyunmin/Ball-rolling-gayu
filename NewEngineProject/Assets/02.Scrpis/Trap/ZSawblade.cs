using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZSawblade : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float movementDistance = 2f;
    public float movementSpeed = 2f;
    public float damageAmount = 10f;

    private Vector3 initialPosition;
    private float movementTimer = 0f;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        // ȸ��
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // �̵�
        movementTimer += Time.deltaTime;
        float Zpostion = Mathf.Sin(movementTimer * movementSpeed) * movementDistance;
        transform.position = initialPosition + new Vector3(0f, 0f, Zpostion);
    }
}
