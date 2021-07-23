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
    public PlayerHitState hitState;
    public PlayerKillState killState;
    public PlayerKnockBackState knockBackState;

    public Rigidbody rigidBody;
    private SpriteRenderer spriteRenderer;
    private Coroutine reposition;
    public IInputable input;
    public Animator animator;
    public GameObject hitTarget;

    [HideInInspector]
    public float health = 40.0f;
    public float maxHealth = 40.0f;


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
        hitState = new PlayerHitState(this);
        killState = new PlayerKillState(this);
        knockBackState = new PlayerKnockBackState(this);

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

    public void SetHitTarget(GameObject target)
    {
        this.hitTarget = target;
    }

    public GameObject GetHitTarget()
    {
        return this.hitTarget;
    }

    public void Hit()
    {
        health -= 1.0f;

        if (stateMachine.GetCurrentState() == hitState)
        {
            hitState.Increment();
        }
        else
        {
            stateMachine.ChangeState(hitState);
        }
    }
}

public enum PlayerState
{
    INITIAL,
    STANDING,
    RUNNING,
    JUMPING,
}
