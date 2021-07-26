using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraConfineSwitcher : MonoBehaviour
{
    public GameObject colliderSwitch;
    public CinemachineVirtualCamera virtualCamera;
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

       CinemachineConfiner confiner = virtualCamera.GetComponent<CinemachineConfiner>(); 
       confiner.InvalidatePathCache();
       confiner.m_BoundingVolume = colliderSwitch.GetComponent<Collider>();
       confiner.InvalidatePathCache();
    }
}
