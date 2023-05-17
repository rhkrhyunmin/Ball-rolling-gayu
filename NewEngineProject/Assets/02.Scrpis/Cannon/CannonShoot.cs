using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShoot : MonoBehaviour
{
    public GameObject ballPrefab;
    [SerializeField] private float minForceMagnitude = 10f; // 최소 힘의 크기
    [SerializeField] private float maxForceMagnitude = 30f; // 최대 힘의 크기
    [SerializeField] private float chargeRate = 1f; // 차징 속도

    private float currentForceMagnitude = 0f; // 현재 힘의 크기
    private bool isCharging = false; // 차징 중인지 여부
    private float chargeStartTime = 0f; // 차징 시작 시간
    public Transform cannonExit; // 대포의 입구 위치

    private void Update()
    {
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

        GameObject ballInstance = Instantiate(ballPrefab, cannonExit.position, Quaternion.identity);
        Rigidbody ballRb = ballInstance.GetComponent<Rigidbody>();
        ballRb.AddForce(forceVector, ForceMode.Impulse);

        currentForceMagnitude = 0f;
    }

    private void UpdateCharge()
    {
        float chargeTime = Time.time - chargeStartTime;
        currentForceMagnitude = Mathf.Clamp(chargeTime * chargeRate, 0f, 1f);
    }
}
