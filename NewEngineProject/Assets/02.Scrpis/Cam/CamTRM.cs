using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTRM : MonoBehaviour
{
    [SerializeField] private Transform EndTrm; //이걸 할려면 End값을 어디에다가 해야하는지 정해야하는데 아직 못정함 
    [SerializeField] private float Movespeed = 8;
    [SerializeField] private float RotationSpeed = 5f;

    private float StartX, EndX;

    private void Awake()
    {
        EndTrm = transform.Find("EndTrm");
        StartX = transform.position.x;
        EndX = EndTrm.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");

        // 이동 로직
        float xData = xMove * Time.deltaTime * Movespeed;
        Vector3 xPos = transform.position + new Vector3(xData, 0, 0);
        xPos.x = Mathf.Clamp(xPos.x, StartX, EndX);
        transform.position = xPos;

        // 회전 로직
        float rotationAmount = -xMove * Time.deltaTime * RotationSpeed;
        transform.Rotate(0, 0, rotationAmount);
    }
}
