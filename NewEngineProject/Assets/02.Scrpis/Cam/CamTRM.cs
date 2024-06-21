using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTRM : MonoBehaviour
{
    [SerializeField] private Transform EndTrm; //�̰� �ҷ��� End���� ��𿡴ٰ� �ؾ��ϴ��� ���ؾ��ϴµ� ���� ������ 
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
       

    }
}
