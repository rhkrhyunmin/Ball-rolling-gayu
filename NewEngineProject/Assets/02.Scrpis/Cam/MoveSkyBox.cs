using UnityEngine;

public class MoveSkyBox : MonoBehaviour
{
    public Skybox _Skybox;
    public float rotationSpeed = 1.0f;

    void Update()
    {
        // ���� ��ī�̹ڽ� ȸ������ �����մϴ�.
        Quaternion currentRotation = _Skybox.transform.rotation;

        // ��ī�̹ڽ��� ���ο� ȸ�������� �����մϴ�.
        _Skybox.transform.rotation = currentRotation * Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
    }
}
