using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraConfineSwitcher : MonoBehaviour
{
    public GameObject colliderSwitch;
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineVirtualCamera swapCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
       if (other.tag != "Player") {
           return;
       }

       CameraSwap.SwapCamera(swapCamera);
    }
}
