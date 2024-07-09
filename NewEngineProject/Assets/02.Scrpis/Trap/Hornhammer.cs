using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hornhammer : MonoBehaviour
{
    public TrapSo trapSo;
    public directionRotation directionRotation; // Enum 정의 필요
    public InitialMovementDirection initialMovementDirection; // Enum 정의 필요

    private Vector3 initialPosition;
    private float movementTimer = 0f;
    private float initialMovementMultiplier;

    private void Start()
    {
        initialPosition = transform.position;
        initialMovementMultiplier = (initialMovementDirection == InitialMovementDirection.Left) ? -1f : 1f;
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * trapSo.rotationSpeed * Time.deltaTime);

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
        movementTimer += Time.deltaTime;
        float xPosition = Mathf.Sin(movementTimer * trapSo.MovementSpeed) * trapSo.movementDistance * initialMovementMultiplier;
        transform.position = initialPosition + new Vector3(xPosition, 0f, 0f);
    }

    private void ZRotation()
    {
        movementTimer += Time.deltaTime;
        float zPosition = Mathf.Sin(movementTimer * trapSo.MovementSpeed) * trapSo.movementDistance * initialMovementMultiplier;
        transform.position = initialPosition + new Vector3(0f, 0f, zPosition);
    }
}
