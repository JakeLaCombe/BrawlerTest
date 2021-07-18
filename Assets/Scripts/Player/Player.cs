using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    public StateMachine stateMachine;
    public PlayerFightingState fightingState;
    public PlayerJumpingState jumpingState;
    public PlayerMovingState movingState;
    public Rigidbody rigidBody;
    private SpriteRenderer spriteRenderer;
    private Coroutine reposition;
    public IInputable input;
    public Animator animator;
    public GameObject hitTarget;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        input = GetComponent<IInputable>();
        stateMachine = new StateMachine();

        fightingState = new PlayerFightingState(this);
        jumpingState = new PlayerJumpingState(this);
        movingState = new PlayerMovingState(this);

        stateMachine.ChangeState(movingState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
      
        UpdateShadow();
       
    }

    private void UpdateShadow()
    {
        GameObject shadow = this.transform.Find("Shadow").gameObject;
        LayerMask floorMask = LayerMask.GetMask("Platforms");
        RaycastHit hit;
        
        if (Physics.Raycast(this.transform.position, new Vector3(0.0f, -10.0f, 0.0f), out hit, 20, floorMask))
        {
            shadow.transform.position = hit.point - new Vector3(0.0f, 0.5f, 0.0f);
        }

        if (this.transform.position.x > 8 && reposition == null && GameObject.Find("VirtualCamera").activeSelf)
        {
            CinemachineVirtualCamera camera = GameObject.Find("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
            camera.Follow = null;
            reposition = StartCoroutine(Reposition());
        }

        if (this.transform.position.y - Camera.main.transform.position.y < -Camera.main.orthographicSize * 2)
        {
            this.transform.position = new Vector3(
                this.transform.position.x,
                12,
                this.transform.position.z
            );
        }
    }

    private IEnumerator Reposition()
    {
        yield return new WaitForSeconds(5.0f);
        GameObject.Find("VirtualCamera").SetActive(false);
        CinemachineVirtualCamera camera = GameObject.Find("VirtualCameraSecondary").GetComponent<CinemachineVirtualCamera>();
    }

    private IEnumerator StopPunch()
    {

        animator.SetBool("isPunching", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("isPunching", false);
    }

    public void SetHitTarget(GameObject target)
    {
        this.hitTarget = target;
    }

    public GameObject GetHitTarget()
    {
        return this.hitTarget;
    }
}

public enum PlayerState
{
    INITIAL,
    STANDING,
    RUNNING,
    JUMPING,
}
