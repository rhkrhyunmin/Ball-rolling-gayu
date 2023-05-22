using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thornbat : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float movementDistance = 2f;
    public float movementSpeed = 2f;

    private Vector3 initialPosition;
    private float movementTimer = 0f;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        // 회전
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        // 이동
        movementTimer += Time.deltaTime;
        float zPosition = Mathf.Sin(movementTimer * movementSpeed) * movementDistance;
        transform.position = initialPosition + new Vector3(0f, 0f, zPosition);
    }
}
