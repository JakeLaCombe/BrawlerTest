using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Light Collision");
        Debug.Log(other.gameObject.name);
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("Left the collision");
        Debug.Log(other.tag);
    }
}
