using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CannonShoot : MonoBehaviour
{
   public GameObject ballPrefab;
    [SerializeField] private float minForceMagnitude = 1f; // �ּ� ���� ũ��
    [SerializeField] private float maxForceMagnitude = 30f; // �ִ� ���� ũ��
    [SerializeField] private float chargeRate = 1f; // ��¡ �ӵ�

    private float currentForceMagnitude = 0f; // ���� ���� ũ��
    private bool isCharging = false; // ��¡ ������ ����
    public Transform cannonExit; // ������ �Ա� ��ġ
    private float chargeTime;

    public void Fire()
    {
        isCharging = false;
        Vector3 forceVector = cannonExit.forward * currentForceMagnitude;

        UnityEngine.GameObject ballInstance = Instantiate(ballPrefab, cannonExit.position, Quaternion.identity);
        Rigidbody ballRb = ballInstance.GetComponent<Rigidbody>();
        ballRb.AddForce(forceVector, ForceMode.Impulse);
        CameraManger.instance.SetActiveCam(CameraCatagory.Ballcam, ballInstance.transform);
        CameraManger.instance.SetFollowTarget(CameraCatagory.Ballcam, ballInstance.transform);
        chargeTime = 0f;
    }

    public void Charge()
    {
        chargeTime += Time.deltaTime;
        currentForceMagnitude = Mathf.Clamp(chargeTime * chargeRate, minForceMagnitude, maxForceMagnitude);
    }
}
