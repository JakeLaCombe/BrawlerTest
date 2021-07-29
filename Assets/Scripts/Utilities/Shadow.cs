using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    private SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();    
    }
    void Update()
    {
        LayerMask floorMask = LayerMask.GetMask("Platforms");
        RaycastHit hit;
        
        if (Physics.Raycast(this.transform.parent.transform.position, new Vector3(0.0f, -10.0f, 0.0f), out hit, 20, floorMask))
        {
            this.transform.position = hit.point - new Vector3(0.0f, 0.5f, 0.0f);
            renderer.enabled = true;
        }
        else
        {
            renderer.enabled = false;
        }
    }
}
