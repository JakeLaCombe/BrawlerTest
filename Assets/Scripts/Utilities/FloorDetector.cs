using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetector : MonoBehaviour
{
    
    private string floorTag = "";// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        floorTag = other.tag;
    }

    private void OnTriggerExit(Collider other) {
        floorTag ="";
    }

    public string GetFloor()
    {
        return floorTag;
    }
}
