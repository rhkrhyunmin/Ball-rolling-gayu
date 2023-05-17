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
public struct CameraSet { public CameraCatagory catagory; public CinemachineVirtualCamera Vcam; }

public class CameraManger : MonoBehaviour
{
    public static CameraManger instance;
    private List<CameraSet> camlist;

    private void Awake()
    {
        if (instance != null)
        {

        }
        instance = this;

        camlist = new List<CameraSet>();

        var cannonCam = transform.Find("CannonCam").GetComponent<CinemachineVirtualCamera>();
        var rigCam = transform.Find("RigCam").GetComponent<CinemachineVirtualCamera>();
        var ballcam = transform.Find("BallCam").GetComponent<CinemachineVirtualCamera>();

        camlist.Add(new CameraSet { catagory = CameraCatagory.CannonCam, Vcam = cannonCam });
        camlist.Add(new CameraSet { catagory = CameraCatagory.Rigcam, Vcam = rigCam });
        camlist.Add(new CameraSet { catagory = CameraCatagory.Ballcam, Vcam = ballcam });
    }

    public void SetFollowTarget(CameraCatagory cat, Transform target)
    {
        foreach (CameraSet camSet in camlist)
        {
            if (camSet.catagory == cat)
            {
                camSet.Vcam.m_Follow = target;
                break;
            }
        }
    }

    public void SetActiveCam(CameraCatagory cat, Transform followTarget = null)
    {
        foreach (CameraSet camset in camlist)
        {
            if (camset.catagory == cat)
            {
                camset.Vcam.Priority = 15;
                if (followTarget != null)
                {
                    camset.Vcam.m_Follow = followTarget;
                }
            }
            else
            {
                camset.Vcam.Priority = 10;
            }
        }
    }
}
