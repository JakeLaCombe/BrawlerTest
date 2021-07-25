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

    public Rigidbody rigidBody;
    public Animator animator;


    public GameObject hitTarget;


    [HideInInspector]
    public float health = 40.0f;
    public float maxHealth = 40.0f;

    void Start()
    {
        health = 40.0f;
        rigidBody = GetComponent<Rigidbody>();
        stateMachine = new StateMachine();
        animator = GetComponent<Animator>();

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
    }

    public bool isDead()
    {
        return stateMachine.GetCurrentState() == killState;
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
}
