using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShoot : MonoBehaviour
{
    public Rigidbody ballPrefab;
    public float forceMultiplier = 1f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 프리팹 복제
            Rigidbody ballInstance = Instantiate(ballPrefab, transform.position, Quaternion.identity);

            // 힘의 크기 계산
            float forceMagnitude = CalculateForceMagnitude();

            // 힘 적용
            Vector3 force = new Vector3(0f, 0f, forceMagnitude);
            ballInstance.AddForce(force);
        }
    }

    private float CalculateForceMagnitude()
    {
        // 힘의 크기를 계산하는 로직을 추가합니다.
        // 예를 들어, 키를 누르는 시간에 따라 힘의 크기를 변화시킬 수 있습니다.
        return forceMultiplier;  // 힘의 크기에 곱할 수를 반환합니다.
    }
}
