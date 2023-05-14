using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMove : MonoBehaviour
{
    public GameObject projectilePrefab;    // 발사체 프리팹
    public Transform spawnPoint;           // 발사 위치
    public float fireForce = 500.0f;       // 발사 속도
    public float rotateSpeed = 50.0f;      // 회전 속도
    public float minAngle = -80.0f;        // 최소 각도
    public float maxAngle = 80.0f;         // 최대 각도

    private float currentAngle;            // 현재 각도

    void Start()
    {
        currentAngle = 0.0f;    // 최초 각도는 0
    }

    void Update()
    {
        // 좌우 회전
        float horizontal = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontal * rotateSpeed * Time.deltaTime);

        // 상하 회전
        float vertical = Input.GetAxis("Vertical");
        float targetAngle = currentAngle + vertical * rotateSpeed * Time.deltaTime;
        targetAngle = Mathf.Clamp(targetAngle, minAngle, maxAngle);

        transform.localRotation = Quaternion.Euler(-targetAngle, transform.localRotation.eulerAngles.y, 0);
        currentAngle = targetAngle;

        // 발사
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * fireForce);
        }
    }
}
