using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTRM : MonoBehaviour
{
    [SerializeField] private Transform EndTrm; //�̰� �ҷ��� End���� ��𿡴ٰ� �ؾ��ϴ��� ���ؾ��ϴµ� ���� ������ 
    [SerializeField] private float Movespeed = 8;

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

        float YMove = Input.GetAxisRaw("Vertical");

        float Data = xMove * Time.deltaTime * Movespeed;
        Vector3 xPos = transform.position + new Vector3(Data, 0, 0);
        xPos.x = Mathf.Clamp(xPos.x, StartX, EndX);
        transform.position = xPos;
    }
}