using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public StateMachine stateMachine;
    public EnemyChaseState chaseState;
    public EnemyHitState hitState;
    public EnemyAttackState attackState;
    public EnemyKillState killState;
    public EnemyKnockBackState knockBackState;
    public bool isMoonLightCharged = false;

    public Rigidbody rigidBody;
    public SpriteRenderer[] spriteRenderers;

    public SpriteRenderer mainRenderer;
    public Shadow shadow;
    public Animator animator;
    public FloorDetector floorDetectorUp;
    public FloorDetector floorDetectorDown;
    public FloorDetector floorDetectorHorizontal;


    public GameObject hitTarget;


    [HideInInspector]
    public float health = 40.0f;
    public float maxHealth = 40.0f;

    void Start()
    {
        health = 40.0f;
        rigidBody = GetComponent<Rigidbody>();
        stateMachine = new StateMachine();
        animator = GetComponentInChildren<Animator>();
        shadow = GetComponentInChildren<Shadow>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        mainRenderer = this.transform.Find("Sprite").GetComponent<SpriteRenderer>();
        floorDetectorUp = this.transform.Find("FloorDetectorUp").GetComponentInChildren<FloorDetector>();
        floorDetectorHorizontal = this.transform.Find("FloorDetectorHorizontal").GetComponentInChildren<FloorDetector>();
        floorDetectorDown = this.transform.Find("FloorDetectorDown").GetComponentInChildren<FloorDetector>();

        chaseState = new EnemyChaseState(this);
        hitState = new EnemyHitState(this);
        killState = new EnemyKillState(this);
        knockBackState = new EnemyKnockBackState(this);
        attackState = new EnemyAttackState(this);

        stateMachine.ChangeState(chaseState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();

        UpdateRenderTag();
        UpdateOutOfBounds();
        UpdateMoonCharge();
    }


    private void UpdateMoonCharge()
    {
        if (isMoonLightCharged)
        {
            mainRenderer.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
        else
        {
            mainRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }

    private void UpdateRenderTag()
    {
        if (shadow.GetFloorTag() == "NonFloorPlatform")
        {
            foreach(SpriteRenderer spriteRenderer in spriteRenderers) {
                spriteRenderer.sortingLayerName = "Platforms";
                spriteRenderer.sortingOrder = 1;
            }
        }
        else
        {
            foreach(SpriteRenderer spriteRenderer in spriteRenderers) {
                spriteRenderer.sortingLayerName = "Sprites";
                spriteRenderer.sortingOrder = 1;
            }
        }
    }

    public bool isDead()
    {
        return stateMachine.GetCurrentState() == killState;
    }

    public void Hit()
    {
        if (!isMoonLightCharged)
        {
            health -= 1.0f;
        }

        if (stateMachine.GetCurrentState() == hitState)
        {
            hitState.Increment();
        }
        else
        {
            stateMachine.ChangeState(hitState);
        }
    }

    public void InstantKill(GameObject hitObject)
    {
        if (stateMachine.GetCurrentState() == this.killState)
        {
            return;
        }

        Rigidbody thrownRigidBody = hitObject.transform.GetComponent<Rigidbody>();

        if (thrownRigidBody != null)
        {
            this.killState.isShot = true;
            this.killState.shootDirection = Mathf.Abs(thrownRigidBody.velocity.x) / thrownRigidBody.velocity.x;
        }

        stateMachine.ChangeState(this.killState);
    }

    public void AttemptAttack()
    {
        if (stateMachine.GetCurrentState() != attackState)
        {
            stateMachine.ChangeState(attackState);
        }
    }

    public void SetHitTarget(GameObject hitTarget)
    {
        this.hitTarget = hitTarget;
    }

    public GameObject getHitTarget()
    {
        return this.hitTarget;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "DeathFloor") {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MoonLight")
        {
            isMoonLightCharged = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MoonLight")
        {
            isMoonLightCharged = false;
        }
    }

    private void UpdateOutOfBounds()
    {
        if (this.transform.position.y - Camera.main.transform.position.y < -Camera.main.orthographicSize * 2)
        {
          Destroy(this.gameObject);
        }
    }
}
