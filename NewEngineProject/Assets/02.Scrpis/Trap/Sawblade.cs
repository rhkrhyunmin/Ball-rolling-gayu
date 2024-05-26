using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum directionRotation
{
    X,
    Z,
}

public class Sawblade : MonoBehaviour
{
    public TrapSo trapSo;
    public directionRotation directionRotation;

    private Vector3 initialPosition;
    private float movementTimer = 0f;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {

        if (directionRotation == directionRotation.X)
        {
            XRotation();
        }
        else if (directionRotation == directionRotation.Z)
        {
            ZRotation();
        }
    }

    private void XRotation()
    {
        transform.Rotate(Vector3.up * trapSo.rotationSpeed * Time.deltaTime);

        // 이동
        movementTimer += Time.deltaTime;
        float xPosition = Mathf.Sin(movementTimer * trapSo.MovementSpeed) * trapSo.movementDistance;
        transform.position = initialPosition + new Vector3(xPosition, 0f, 0f);
    }

    private void ZRotation()
    {
        transform.Rotate(Vector3.up * trapSo.rotationSpeed * Time.deltaTime);

        // 이동
        movementTimer += Time.deltaTime;
        float zPosition = Mathf.Sin(movementTimer * trapSo.MovementSpeed) * trapSo.movementDistance;
        transform.position = initialPosition + new Vector3(0f, 0f, zPosition);
    }
}
