using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO", menuName = "SO/BallSO")]
public class BallSO : ScriptableObject
{
    [Header("Hp")]
    public float maxHp = 0f;
    public float currentHp = 0f;
    public float damageAmount = 3f;

    [Header("Attack")]
    public float dashForce = 0f;          
    public float dashDuration = 0.5f;      
    public float dashCooldown = 0f;        
    public float dashDamage = 0;         
    public float dashKnockbackForce = 0f;

    [Header("Speed")]
    public float moveSpeed = 0f;
}
