using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CannonShoot : MonoBehaviour
{
    public UnityEngine.GameObject ballPrefab;
    [SerializeField] private float minForceMagnitude = 1f; // �ּ� ���� ũ��
    [SerializeField] private float maxForceMagnitude = 30f; // �ִ� ���� ũ��
    [SerializeField] private float chargeRate = 1f; // ��¡ �ӵ�

    CannonRotate cannonRotate;

    private Ground ground;
    private float currentForceMagnitude = 0f; // ���� ���� ũ��
    private bool isCharging = false; // ��¡ ������ ����
    private float chargeStartTime = 0f; // ��¡ ���� �ð�
    public Transform cannonExit; // ������ �Ա� ��ġ

    private void Awake()
    {
        cannonRotate = GetComponent<CannonRotate>();
    }

    private void Update()
    {
        StopGround();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCharging();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Fire();
        }

        if (isCharging)
        {
            UpdateCharge();
        }

    }

    private void StartCharging()
    {
        isCharging = true;
        chargeStartTime = Time.time;
    }

    private void Fire()
    {
        isCharging = false;
        float force = Mathf.Lerp(minForceMagnitude, maxForceMagnitude, currentForceMagnitude);
        Vector3 forceVector = cannonExit.forward * force;

        UnityEngine.GameObject ballInstance = Instantiate(ballPrefab, cannonExit.position, Quaternion.identity);
        ground = ballInstance.GetComponent<Ground>();
        Rigidbody ballRb = ballInstance.GetComponent<Rigidbody>();
        ballRb.AddForce(forceVector, ForceMode.Impulse);

        currentForceMagnitude = 0f;
    }

    private void UpdateCharge()
    {
        float chargeTime = Time.time - chargeStartTime;
        currentForceMagnitude = Mathf.Clamp(chargeTime * chargeRate, 0f, 1f);
    }

    private void StopGround()
    {
        try
        {
            if (ground.IsGround())
            {
                cannonRotate.rotateSpeed = 0;
                Debug.Log("3");
            }

            else if (!ground.IsGround() || cannonRotate.rotateSpeed == 0)
            {
                cannonRotate.rotateSpeed = cannonRotate.rotationSpeed;
            }
        }
        catch(Exception exp)
        {

        }
    }
}
