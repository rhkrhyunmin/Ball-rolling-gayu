using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCannonControl : MonoBehaviour
{
    [SerializeField] public UnityEngine.GameObject CamrigTRm;

    [SerializeField]
    private Transform barrerTRM;
    //public GameObject ballPrefab;
    public Transform firePosition;

    [SerializeField]
    private CannonState currentState;

    OtherCannonShoot cannonShoot;
    CannonRotate cannonRotate;

    public enum CannonState : short
    {
        IDLE = 0,
        MOVING = 1,
        CHARGE = 2,
        FIRE = 3,
        WAITING = 4,
    }

    private void Awake()
    {
        currentState = CannonState.MOVING;
        CamrigTRm = UnityEngine.GameObject.Find("BallCam");
        cannonShoot = GetComponentInChildren<OtherCannonShoot>();
        cannonRotate = GetComponentInChildren<CannonRotate>();
    }

    private void Update()
    {
        if (currentState == CannonState.MOVING)
        {
            cannonRotate.Rotate();
            if (Input.GetButtonDown("Jump"))
            {
                currentState = CannonState.CHARGE;
                //currentPower = 0;
            }
        }
        if (Input.GetButton("Jump") && currentState == CannonState.CHARGE)
        {
            cannonShoot.Charge();
            /*currentPower += chargeSpeed * Time.deltaTime;
            currentPower = Mathf.Clamp(currentPower, 0, 100);*/
        }

        if (Input.GetButtonUp("Jump") && currentState == CannonState.CHARGE)
        {
            currentState = CannonState.FIRE;

            StartCoroutine(FireSequense());
        }
    }

    private void CheckWait()
    {
        if (currentState != CannonState.WAITING) return;

        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine(ChanageToIdle());
        }
    }

    private IEnumerator ChanageToIdle()
    {
        CameraManger.instance.SetActiveCam(CameraCatagory.Rigcam);
        yield return new WaitForSeconds(1f);
        currentState = CannonState.IDLE;
    }


    public void Fire()
    {
        if (currentState != CannonState.CHARGE)
            return;

        currentState = CannonState.FIRE;

        isCharging = false;
        Vector3 forceVector = cannonExit.forward * currentForceMagnitude;

        GameObject ballInstance = Instantiate(ballPrefab, cannonExit.position, Quaternion.identity);

        Rigidbody ballRb = ballInstance.GetComponent<Rigidbody>();
        ballRb.AddForce(forceVector, ForceMode.Impulse);

        BallHp ballHp = ballInstance.GetComponent<BallHp>();

        StartCoroutine(FireSequence(ballInstance));
    }

    private IEnumerator FireSequence(GameObject ballInstance)
    {
        // ���� ķ ��Ȱ��ȭ
        CameraManger.instance.SetActiveCam(CameraCatagory.Rigcam);

        // ���� ķ���κ��� �߻� ��� ���� ���� ����
        CameraManger.instance.StopFollowTarget(CameraCatagory.CannonCam);

        // ���� �߻� �� ��� �ð�
        yield return new WaitForSeconds(1.5f);

        // �߻� ������ ó���� ���⿡ �߰��� �� �ֽ��ϴ�.

        // �߻簡 �Ϸ�Ǿ����Ƿ� ���¸� IDLE�� ����
        currentState = CannonState.IDLE;

        // �߻� ������ ó���� �Ϸ�Ǿ����Ƿ� ���� �����մϴ�.
        Destroy(ballInstance);

        // isCharging ���� �ʱ�ȭ
        isCharging = false;
        currentForceMagnitude = 0f;
    }
}
