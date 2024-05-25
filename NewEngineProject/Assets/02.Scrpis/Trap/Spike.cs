using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public TrapSo trapSo;

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
        float YPosition = Mathf.Cos(movementTimer * trapSo.MovementSpeed) * trapSo.movementDistance;
        transform.position = initialPosition + new Vector3(0f, YPosition, 0f);
    }
}
