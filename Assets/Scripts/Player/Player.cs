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
    public LevelEndJumpState levelEndJumpState;
    public LevelEndMoveState levelEndMoveState;


    public Rigidbody rigidBody;
    public SpriteRenderer spriteRenderer;
    private Coroutine reposition;
    public IInputable input;
    public Animator animator;
    public GameObject attackZone;
    public Shadow shadow;

    public List<GameObject> hitTargets;

    [HideInInspector]
    public float health = 40.0f;

    [HideInInspector]
    public float maxHealth = 40.0f;

    [HideInInspector]
    public int silverCount = 0;

    private bool isRespawning = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        spriteRenderer = this.transform.Find("Sprite").GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        input = GetComponent<IInputable>();
        shadow = GetComponentInChildren<Shadow>();

        stateMachine = new StateMachine();

        hitTargets = new List<GameObject>();

        fightingState = new PlayerFightingState(this);
        jumpingState = new PlayerJumpingState(this);
        movingState = new PlayerMovingState(this);
        hitState = new PlayerHitState(this);
        killState = new PlayerKillState(this);
        knockBackState = new PlayerKnockBackState(this);
        startState = new LevelStartState(this);
        levelEndJumpState = new LevelEndJumpState(this);
        levelEndMoveState = new LevelEndMoveState(this);

        attackZone = this.transform.Find("Player Hit").gameObject;

        stateMachine.ChangeState(startState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
      
        UpdateOutOfBounds();
        UpdateRenderTag();

        if (input.Pause())
        {
            if (Time.timeScale == 0.0f) {
                Time.timeScale = 1.0f;
            } else {
                Time.timeScale = 0.0f;
            }
        }
    }

    private void UpdateRenderTag()
    {
        if (shadow.GetFloorTag() == "NonFloorPlatform")
        {
            shadow.spriteRenderer.sortingLayerID = SortingLayer.NameToID("Platforms");
            shadow.spriteRenderer.sortingOrder = 1;
        }
        else
        {
            shadow.spriteRenderer.sortingLayerID = SortingLayer.NameToID("Sprites");
            shadow.spriteRenderer.sortingOrder = 1;
        }
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
        if (stateMachine.GetCurrentState() == knockBackState || isRespawning)
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

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "DeathFloor") {
            this.transform.position = new Vector3(this.transform.position.x, -2.2f, this.transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other) {
         if (other.gameObject.tag == "LevelComplete") {
            string name =  other.transform.parent.name;

            if (stateMachine.GetCurrentState() != levelEndJumpState) {
                stateMachine.ChangeState(levelEndJumpState);
            }
        }
    }

    public void moveToEnd(Vector3 target)
    {
        levelEndMoveState.SetTarget(target);
        stateMachine.ChangeState(levelEndMoveState);
    }

    public void Revive()
    {
        this.health = 40.0f;
        isRespawning = true;
        stateMachine.ChangeState(startState);
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);

        StartCoroutine(RespawnDone());
    }

    public IEnumerator RespawnDone()
    {
        yield return new WaitForSeconds(5.0f);
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        isRespawning = false;
    }
}

public enum PlayerState
{
    INITIAL,
    STANDING,
    RUNNING,
    JUMPING,
}
