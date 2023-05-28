using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum CameraCatagory
{
    CannonCam = 0,
    Ballcam = 1,
    Rigcam = 2,
}

public class CameraManger : MonoBehaviour
{
    public static CameraManger instance;
    private List<CinemachineVirtualCamera> camlist;

    private void Awake()
    {
        if (instance != null)
        {

        }
        instance = this;

        camlist = new List<CinemachineVirtualCamera>();

        var cannonCam = transform.Find("CannonCam").GetComponent<CinemachineVirtualCamera>();
        var ballcam = transform.Find("BallCam").GetComponent<CinemachineVirtualCamera>();
        var rigCam = transform.Find("RigCam").GetComponent<CinemachineVirtualCamera>();

        camlist.Add(cannonCam);
        camlist.Add(ballcam);
        camlist.Add(rigCam);
    }

    public void SetFollowTarget(CameraCatagory cat, Transform target)
    {
        camlist[(int)cat].m_Follow = target;
        camlist[(int)cat].m_LookAt = target;
    }

    public void SetActiveCam(CameraCatagory cat, Transform followTarget = null)
    {
        for (int i = 0; i < camlist.Count; i++)
        {
            camlist[i].Priority = 10;
        }
        camlist[(int)cat].Priority = 15;
        camlist[(int)cat].m_Follow = followTarget;
    }
}
