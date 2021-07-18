using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public StateMachine stateMachine;
    public EnemyChaseState chaseState;
    public EnemyHitState hitState;
    public EnemyKillState killState;
    public Rigidbody rigidBody;
    public Animator animator;


    [HideInInspector]
    public float health = 10.0f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        stateMachine = new StateMachine();
        animator = GetComponent<Animator>();
        
        chaseState = new EnemyChaseState(this);
        hitState = new EnemyHitState(this);
        killState = new EnemyKillState(this);

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
}
