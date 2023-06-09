using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeletion : MonoBehaviour
{
    public float deletionRadius = 20f; // 삭제 반경을 설정합니다.

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, deletionRadius);

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Ball")) // 태그가 "Ball"인 공을 삭제합니다.
                {
                    Destroy(collider.gameObject);
                }
            }
        }
    }
}
