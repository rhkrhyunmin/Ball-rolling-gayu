using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CannonShoot : MonoBehaviour
{
   public GameObject ballPrefab;
    [SerializeField] private float minForceMagnitude = 1f; // �ּ� ���� ũ��
    [SerializeField] private float maxForceMagnitude = 30f; // �ִ� ���� ũ��
    [SerializeField] private float chargeRate = 1f; // ��¡ �ӵ�

    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private Slider HPslider;
    [SerializeField] private float timer = 0;
    [SerializeField] private TextMeshProUGUI TimerTMP;

    private float currentForceMagnitude = 0f; // ���� ���� ũ��
    private bool isCharging = false; // ��¡ ������ ����
    public Transform cannonExit; // ������ �Ա� ��ġ
    private float chargeTime;

    private void Update()
    {
        Timer();
    }

    public void Timer()
    {
        timer += Time.deltaTime;
        TimerTMP.text = timer.ToString("F2");
    }

    public void Fire()
    {
        HPslider.enabled = false;

        isCharging = false;
        Vector3 forceVector = cannonExit.forward * currentForceMagnitude;

        UnityEngine.GameObject ballInstance = Instantiate(ballPrefab, cannonExit.position, Quaternion.identity);

        Rigidbody ballRb = ballInstance.GetComponent<Rigidbody>();
        ballRb.AddForce(forceVector, ForceMode.Impulse);

        BallHp ballHp = ballInstance.GetComponent<BallHp>();
    
        ballHp.gameOverPanel = gameoverPanel;
        ballHp.healthSlider = HPslider;

        HPslider.enabled = true;

        

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
