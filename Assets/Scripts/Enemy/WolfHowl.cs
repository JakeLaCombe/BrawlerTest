using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfHowl : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigidBody;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody.velocity = new Vector3(0.0f, -1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(rigidBody.velocity.y) <= Mathf.Epsilon)
        {
            animator.SetBool("isAirborne", false);
            animator.SetBool("isInitialized", true);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("wolf_howl_end"))
        {
            rigidBody.velocity = new Vector3(0.0f, 30.0f, 0.0f);
        }

        if(!spriteRenderer.isVisible && rigidBody.velocity.y > 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
