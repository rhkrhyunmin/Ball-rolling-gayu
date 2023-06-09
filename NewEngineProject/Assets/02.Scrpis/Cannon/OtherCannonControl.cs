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
        // 대포 캠 비활성화
        CameraManger.instance.SetActiveCam(CameraCatagory.Rigcam);

        // 대포 캠으로부터 발사 대상에 대한 추적 종료
        CameraManger.instance.StopFollowTarget(CameraCatagory.CannonCam);

        // 대포 발사 후 대기 시간
        yield return new WaitForSeconds(1.5f);

        // 발사 이후의 처리를 여기에 추가할 수 있습니다.

        // 발사가 완료되었으므로 상태를 IDLE로 변경
        currentState = CannonState.IDLE;

        // 발사 이후의 처리가 완료되었으므로 공을 삭제합니다.
        Destroy(ballInstance);

        // isCharging 변수 초기화
        isCharging = false;
        currentForceMagnitude = 0f;
    }
}
