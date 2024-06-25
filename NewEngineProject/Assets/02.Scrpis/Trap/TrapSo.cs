using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO", menuName = "SO/TrapSO")]
public class TrapSo : ScriptableObject
{
    [Header("Distance")]
    public float movementDistance = 0;

    [Header("Speed")]
    public float MovementSpeed = 0;
    public float rotationSpeed = 0;
}
