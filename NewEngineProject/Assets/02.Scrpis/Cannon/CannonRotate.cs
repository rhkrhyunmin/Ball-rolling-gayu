using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotate : MonoBehaviour
{
    public float rotationSpeed = 50f;  // 회전 속도
    public float rotateSpeed;
    Vector3 currentRotation;
    private void Start()
    {
        rotateSpeed = rotationSpeed;
        currentRotation = transform.localRotation.eulerAngles;
    }

    public void Rotate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float yRotation = verticalInput * rotateSpeed * Time.deltaTime;
        float zRotation = horizontalInput * rotateSpeed * Time.deltaTime;

        currentRotation.y += yRotation;
        currentRotation.z += zRotation;

        currentRotation.y = Mathf.Clamp(currentRotation.y, -20f, 20f);
        currentRotation.z = Mathf.Clamp(currentRotation.z, -30f, 30f);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }
}
