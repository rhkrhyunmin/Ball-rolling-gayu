using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotate : MonoBehaviour
{
    private float rotationSpeed = 100f;  // ȸ�� �ӵ�
    private float rotateSpeed;
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
        currentRotation.z = Mathf.Clamp(currentRotation.z, -20f, 30f);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }
}
