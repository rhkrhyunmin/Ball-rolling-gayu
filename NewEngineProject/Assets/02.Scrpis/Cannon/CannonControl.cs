using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CannonControl : MonoBehaviour
{
    [SerializeField] public UnityEngine.GameObject CamrigTRm;

    [SerializeField]
    private Transform barrerTRM;
    //public GameObject ballPrefab;
    public Transform firePosition;

    [SerializeField]
    private CannonState currentState;

    CannonShoot cannonShoot;
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
        cannonShoot = GetComponentInChildren<CannonShoot>();
        cannonRotate = GetComponentInChildren<CannonRotate>();
    }

    private void Update()
    {
        if(currentState == CannonState.MOVING)
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


    private IEnumerator FireSequense()
    {
        CameraManger.instance.SetActiveCam(CameraCatagory.CannonCam);
        CameraManger.instance.SetFollowTarget(CameraCatagory.CannonCam, barrerTRM);
        yield return new WaitForSeconds(1.5f);
        cannonShoot.Fire();
    }
}