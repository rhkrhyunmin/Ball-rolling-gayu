using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class CannonShoot : MonoBehaviour
{
    public GameObject ballPrefab;
    public CameraShake cameraShake;
    public ParticleSystem boomEffect;
    [Header("chargingvalue")]
    [SerializeField] private float minForceMagnitude = 1f; // �ּ� ���� ũ��
    [SerializeField] private float maxForceMagnitude = 30f; // �ִ� ���� ũ��
    [SerializeField] private float chargeRate = 1f; // ��¡ �ӵ�
    private float currentForceMagnitude = 0f; // ���� ���� ũ��

    private float timer = 0;

    
    private bool isCharging = false; // ��¡ ������ ����
    public Transform cannonExit; // ������ �Ա� ��ġ
    private float chargeTime;
    
    private LineRenderer lineRenderer;
    Vector3 startPosition;
    Vector3 startVelocity;

    int i = 0;
    private int numPoints = 30; // ���� ����� ���� ����
    float lineTimer = 0.3f;

    public AudioClip bombclip;
    private AudioSource audioSource;

   // private Vector3[] points; // ������ ����� ����� ���� �迭

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Timer();
        if (isCharging)
        {
            //UpdateLineRenderer();

        }
    }

    public void Timer()
    {
        timer += Time.deltaTime;
    }

    public void Fire()
    {
        boomEffect.Play();
        UIManager.Instance.hpSlider.enabled = false;
        UIManager.Instance.speedImage.gameObject.SetActive(true);
        UIManager.Instance.hpImage.gameObject.SetActive(true);

        isCharging = false;
        Vector3 forceVector = cannonExit.forward * currentForceMagnitude;

        GameObject ballInstance = Instantiate(ballPrefab, cannonExit.position, Quaternion.identity);

        Rigidbody ballRb = ballInstance.GetComponent<Rigidbody>();
        ballRb.AddForce(forceVector, ForceMode.Impulse);

        PlayerHp ballHp = ballInstance.GetComponent<PlayerHp>();

        //ballHp. = UIManager.Instance.hpSlider;

        UIManager.Instance.hpSlider.enabled = true;

        CameraManger.instance.SetActiveCam(CameraCatagory.Ballcam, ballInstance.transform);
        CameraManger.instance.SetFollowTarget(CameraCatagory.Ballcam, ballInstance.transform);
        chargeTime = 0f;

        Destroy(lineRenderer);
        audioSource.PlayOneShot(bombclip);
    }

    public void Charge()
    {
        chargeTime += Time.deltaTime;
        currentForceMagnitude = Mathf.Clamp(chargeTime * chargeRate, minForceMagnitude, maxForceMagnitude);
        isCharging = true;
        DrawLine();
        //UpdateLineRenderer();
    }

    void DrawLine()
    {
        i = 0;
        lineRenderer.positionCount = numPoints;
        lineRenderer.enabled = true;
        startPosition = cannonExit.position;
        startVelocity = currentForceMagnitude * cannonExit.transform.forward;
        lineRenderer.SetPosition(i, startPosition);
        for (float j = 0; i < lineRenderer.positionCount - 1; j += lineTimer)
        {
            i++;
            Vector3 linePosition = startPosition + j * startVelocity;
            linePosition.y = startPosition.y + startVelocity.y * j + 0.5f * Physics.gravity.y * j * j;
            lineRenderer.SetPosition(i, linePosition);
        }
    }
}


   /* private void UpdateLineRenderer()
    {
        CalculateBezierCurve();
        lineRenderer.positionCount = numPoints;
        lineRenderer.SetPositions(points);

    }*/

   /* private void CalculateBezierCurve()
    {
        Vector3 startPosition = cannonExit.position;
        Vector3 endPosition = cannonExit.position + cannonExit.forward * currentForceMagnitude * 0.2f;
        Vector3 controlPosition = cannonExit.position + cannonExit.forward * currentForceMagnitude * 0.5f;

        // ������ ��� ����Ͽ� points �迭�� ����
        float tStep = 1f / (numPoints - 1);
        float t = 0f;
        for (int i = 0; i < numPoints; i++)
        {
            points[i] = CalculateBezierPoint(t, startPosition, endPosition, controlPosition);
            t += tStep;
        }
    }*/

    /*private Vector3 CalculateBezierPoint(float t, Vector3 start, Vector3 end, Vector3 control)
    {
        // ������ ��� ���� ���
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 point = uuu * start +
                        3f * uu * t * control +
                        3f * u * tt * end +
                        ttt * end;
        return point;
    }*/
//}
