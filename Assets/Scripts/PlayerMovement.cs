using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigidBody;
    Vector3 lastWalkVector = Vector3.zero;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float vx = 0;
        float vy = rigidBody.velocity.y;
        float vz = 0;

        if (Input.GetKey(KeyCode.LeftArrow)) {
            vx = -3.0f;
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            vx = 3.0f;
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            vz = 3.0f;
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            vz = -3.0f;
        }

        if (Input.GetKeyDown(KeyCode.Return) &&  rigidBody.velocity.y <= Mathf.Abs(float.Epsilon)) {
            vy = 5.0f;
        }

        rigidBody.velocity = new Vector3(
           vx,
           vy,
           vz
        );
    }
}
