using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
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
        // ¿Ãµø
        movementTimer += Time.deltaTime;
        float YPosition = Mathf.Cos(movementTimer * movementSpeed) * movementDistance;
        transform.position = initialPosition + new Vector3(0f, YPosition, 0f);
    }
}
