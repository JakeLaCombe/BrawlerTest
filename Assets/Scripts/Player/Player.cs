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
    public LevelStartState startState;

    public Rigidbody rigidBody;
    private SpriteRenderer spriteRenderer;
    private Coroutine reposition;
    public IInputable input;
    public Animator animator;
    public GameObject attackZone;

    public List<GameObject> hitTargets;

    [HideInInspector]
    public float health = 40.0f;

    [HideInInspector]
    public float maxHealth = 40.0f;

    [HideInInspector]
    public int silverCount = 0;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        input = GetComponent<IInputable>();
        stateMachine = new StateMachine();

        hitTargets = new List<GameObject>();

        fightingState = new PlayerFightingState(this);
        jumpingState = new PlayerJumpingState(this);
        movingState = new PlayerMovingState(this);
        hitState = new PlayerHitState(this);
        killState = new PlayerKillState(this);
        knockBackState = new PlayerKnockBackState(this);
        startState = new LevelStartState(this);

        attackZone = this.transform.Find("Player Hit").gameObject;

        stateMachine.ChangeState(startState);
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

    public void AddHitTarget(GameObject target)
    {
        this.hitTargets.Add(target);
    }

    public void RemoveHitTarget(GameObject target)
    {
        this.hitTargets.Remove(target);
    }

    public List<GameObject> GetHitTargets()
    {
        return this.hitTargets;
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

    public void AddSilver()
    {
        silverCount += 1;
    }

    public bool isPlayerControlled()
    {
        return this.stateMachine.GetCurrentState() != startState;
    }
}

public enum PlayerState
{
    INITIAL,
    STANDING,
    RUNNING,
    JUMPING,
}
