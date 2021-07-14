using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour, IInputable
{
    public bool Up()
    {
        return Input.GetKeyDown(KeyCode.UpArrow);
    }
    public bool UpHold()
    {
        return Input.GetKey(KeyCode.UpArrow);
    }
    public bool Down()
    {
        return Input.GetKeyDown(KeyCode.DownArrow);
    }
    public bool DownHold()
    {
        return Input.GetKey(KeyCode.DownArrow);
    }
    public bool Left()
    {
        return Input.GetKeyDown(KeyCode.LeftArrow);
    }
    public bool LeftHold()
    {
        return Input.GetKey(KeyCode.LeftArrow);
    }

    public bool Right()
    {
        return Input.GetKeyDown(KeyCode.RightArrow);
    }

    public bool RightHold()
    {
        return Input.GetKey(KeyCode.RightArrow);
    }

    public bool Jump()
    {
        return Input.GetKey(KeyCode.X);
    }

    public bool Pause()
    {
        return Input.GetKeyDown(KeyCode.Return);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
