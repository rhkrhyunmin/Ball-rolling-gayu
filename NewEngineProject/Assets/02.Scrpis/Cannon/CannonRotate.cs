using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotate : MonoBehaviour
{
    public float rotationSpeed = 50f;  // 회전 속도
    Vector3 currentRotation;
    private void Start()
    {
        currentRotation = transform.localRotation.eulerAngles;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float yRotation = verticalInput * rotationSpeed * Time.deltaTime;
        float zRotation = horizontalInput * rotationSpeed * Time.deltaTime;

        currentRotation.y += yRotation;
        currentRotation.z += zRotation;

        currentRotation.y = Mathf.Clamp(currentRotation.y, -20f, 20f);
        currentRotation.z = Mathf.Clamp(currentRotation.z, -30f, 30f);
        transform.localRotation = Quaternion.Euler(currentRotation);

    }
}
