using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotate : MonoBehaviour
{
    public float rotateSpeed = 10f;
    public float minAngleY = -45f;
    public float maxAngleY = 45f;
    public float minAngleZ = -45f;
    public float maxAngleZ = 45f;

    void Start()
    {
        // �ʱ� �����̼� ����
        transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
    }

    void Update()
    {
        float angleY = transform.localRotation.eulerAngles.y;
        float angleZ = transform.localRotation.eulerAngles.z;

        // ĳ�� y�� ȸ�� ó��
        float verticalInput = Input.GetAxis("Vertical");
        if (verticalInput > 45 && angleY < maxAngleY)
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
            Debug.Log("1");
        }
        else if (verticalInput < 0 && angleY > minAngleY)
        {
            transform.Rotate(Vector3.down, rotateSpeed * Time.deltaTime);
            Debug.Log("2");
        }

        // ĳ�� z�� ȸ�� ó��
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput > 45 && angleZ < maxAngleZ)
        {
            transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
            Debug.Log("3");
        }
       
        else if (horizontalInput < 0 && angleZ > minAngleZ)
        {
            transform.Rotate(Vector3.back, rotateSpeed * Time.deltaTime);
            Debug.Log("4");
        }
    }
}
