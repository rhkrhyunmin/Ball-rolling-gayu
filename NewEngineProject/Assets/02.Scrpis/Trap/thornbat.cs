using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thornbat : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float movementDistance = 2f;
    public float movementSpeed = 2f;
    public float damageAmount = 10f;


    private Vector3 initialPosition;
    private float movementTimer = 0f;
     
    //private GameObject GameOverPanel;

    private void Start()
    {
        initialPosition = transform.position;
        //GameOverPanel.SetActive(false);
    }

    private void Update()
    {
        // 회전
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        // 이동
        movementTimer += Time.deltaTime;
        float zPosition = Mathf.Sin(movementTimer * movementSpeed) * movementDistance;
        transform.position = initialPosition + new Vector3(zPosition, 0f, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            
        }
    }
}