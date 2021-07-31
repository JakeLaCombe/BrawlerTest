using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwap
{
   public static void SwapCamera(CinemachineVirtualCamera nextCamera)
    {
       CinemachineBrain brain = Camera.main.GetComponent<CinemachineBrain>();
       brain.ActiveVirtualCamera.Priority = 0;
       brain.ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);
       nextCamera.Priority = 10;
       nextCamera.gameObject.SetActive(true);       
    }
}