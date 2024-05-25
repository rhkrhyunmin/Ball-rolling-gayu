using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO", menuName = "SO/EnemySO")]
public class EnemySO : ScriptableObject
{
    [Header("Distance")]
    public float DetectionDistance = 0;
    public float AttackDistance = 0;

    [Header("Speed")]
    public float MovementSpeed = 0;
    public float AttackCoolDonw = 0;

    [Header("Damage")]
    public float AttackDamage = 0;

    [Header("Hp")]
    public float currentHp = 0;
    public float MaxHp = 0;
}
