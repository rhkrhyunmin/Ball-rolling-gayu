using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBall : MonoBehaviour
{
    private Vector3 originalScale;   // ���� ũ��

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale; // �ʱ� ũ�� ����
    }
}
