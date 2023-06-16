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
        // 회전
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // 이동
        movementTimer += Time.deltaTime;
        float Zpostion = Mathf.Sin(movementTimer * movementSpeed) * movementDistance;
        transform.position = initialPosition + new Vector3(0f, 0f, Zpostion);
    }
}
