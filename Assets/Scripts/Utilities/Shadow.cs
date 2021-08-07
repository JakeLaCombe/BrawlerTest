using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    private string floorTag;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }
    void Update()
    {
        LayerMask floorMask = LayerMask.GetMask("Platforms");
        RaycastHit hit;
        
        if (Physics.Raycast(this.transform.parent.transform.position, new Vector3(0.0f, -10.0f, 0.0f), out hit, 20, floorMask))
        {
            this.transform.position = hit.point - new Vector3(0.0f, 0.5f, 0.0f);
            spriteRenderer.enabled = true;
            floorTag = hit.transform.gameObject.tag;
        }
        else
        {
            spriteRenderer.enabled = false;
            floorTag = "";
        }
    }

    public string GetFloorTag()
    {
        return floorTag;
    }
}
