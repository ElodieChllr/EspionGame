using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraSwitch 
{
    static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();

    public static CinemachineVirtualCamera ActiveCamera = null;

    public static bool IsActiveCam(CinemachineVirtualCamera cam)
    {
        return cam == ActiveCamera;
    }
    public static void Register(CinemachineVirtualCamera cam)
    {
        cameras.Add(cam);
    }

    public static void Unregister(CinemachineVirtualCamera cam)
    {
        cameras.Remove(cam);
    }


    public static void Switch(CinemachineVirtualCamera cam)
    {
        cam.Priority = 10;
        ActiveCamera = cam;

        foreach(CinemachineVirtualCamera c in cameras)
        {
            if( c!= cam && c.Priority != 0)
            {
                c.Priority = 0;
            }
        }
    }









}
