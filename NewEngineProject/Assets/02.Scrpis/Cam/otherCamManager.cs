using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class otherCamManager : MonoBehaviour
{
    public static otherCamManager instance;

    [SerializeField] private CameraData[] cameraDataArray;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SetActiveCam(CameraCatagory category, Transform target = null)
    {
        CameraData cameraData = GetCameraData(category);
        if (cameraData != null)
        {
            cameraData.cameraObject.SetActive(true);
            if (target != null)
            {
                cameraData.cameraTransform.position = target.position;
                cameraData.cameraTransform.rotation = target.rotation;
            }
        }
    }

    public void SetFollowTarget(CameraCatagory category, Transform target)
    {
        CameraData cameraData = GetCameraData(category);
        if (cameraData != null)
        {
            cameraData.cameraTransform.parent = target;
            cameraData.cameraTransform.localPosition = Vector3.zero;
            cameraData.cameraTransform.localRotation = Quaternion.identity;
        }
    }

    public void StopFollowTarget(CameraCatagory category)
    {
        CameraData cameraData = GetCameraData(category);
        if (cameraData != null)
        {
            cameraData.cameraTransform.parent = null;
        }
    }

    private CameraData GetCameraData(CameraCatagory category)
    {
        foreach (CameraData data in cameraDataArray)
        {
            if (data.cameraCategory == category)
            {
                return data;
            }
        }

        return null;
    }
}

[System.Serializable]
public class CameraData
{
    public CameraCatagory cameraCategory;
    public GameObject cameraObject;
    public Transform cameraTransform;
}

public enum CameraCatagory1
{
    Rigcam,
    CannonCam,
    Ballcam
}
