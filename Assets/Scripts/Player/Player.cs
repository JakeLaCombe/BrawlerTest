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
      
        UpdateOutOfBounds();
    }

    private void UpdateOutOfBounds()
    {
        if (this.transform.position.y - Camera.main.transform.position.y < -Camera.main.orthographicSize * 2)
        {
            this.transform.position = new Vector3(
                this.transform.position.x,
                12,
                this.transform.position.z
            );
        }
    }

    public void SetHitTarget(GameObject target)
    {
        this.hitTarget = target;
    }

    public GameObject GetHitTarget()
    {
        return this.hitTarget;
    }

    public void Hit(Vector3 hitDirection)
    {
        if (stateMachine.GetCurrentState() == knockBackState)
        {
            return;
        }

        health -= 1.0f;
        hitState.SetDirection(hitDirection);

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
