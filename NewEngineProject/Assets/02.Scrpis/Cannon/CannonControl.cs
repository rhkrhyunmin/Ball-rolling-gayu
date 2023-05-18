using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CannonControl : MonoBehaviour
{
    [SerializeField] public UnityEngine.GameObject CamrigTRm;

    private Transform barrerTRM;
    public Transform ballPrefab;
    private Transform firePosition;

    [SerializeField]
    private CannonState currentState;

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
        currentState = CannonState.IDLE;
        barrerTRM = transform.Find("canon");
        CamrigTRm = UnityEngine.GameObject.Find("BallCam");
    }

    private void Update()
    {
        if ((short)currentState < 2)
        {

        }
        CheckFire();
        CheckWait();
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

    private void CheckFire()
    {
        if (Input.GetButtonDown("Jump") && (short)currentState < 2)
        {
            currentState = CannonState.CHARGE;
            //currentPower = 0;
        }

        if (Input.GetButton("Jump") && currentState == CannonState.CHARGE)
        {
            /*currentPower += chargeSpeed * Time.deltaTime;
            currentPower = Mathf.Clamp(currentPower, 0, 100);*/
        }

        if (Input.GetButtonUp("Jump") && currentState == CannonState.CHARGE)
        {
            currentState = CannonState.FIRE;

            StartCoroutine(FireSequense());
        }
    }

    private IEnumerator FireSequense()
    {
        CameraManger.instance.SetActiveCam(CameraCatagory.CannonCam);
        CameraManger.instance.SetFollowTarget(CameraCatagory.Ballcam, barrerTRM);
        yield return new WaitForSeconds(1.5f);

        CamrigTRm.transform.localPosition = Vector3.zero;
        Debug.Log("4");
        Transform ball = Instantiate(ballPrefab, firePosition.position, Quaternion.identity);
        
        CameraManger.instance.SetActiveCam(CameraCatagory.Ballcam, ball.transform);
    }
}