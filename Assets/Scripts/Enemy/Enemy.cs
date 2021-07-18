using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public StateMachine stateMachine;
    public EnemyChaseState chaseState;
    public Rigidbody rigidBody;
    public Animator animator;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        stateMachine = new StateMachine();
        animator = GetComponent<Animator>();
        
        chaseState = new EnemyChaseState(this);
        stateMachine.ChangeState(chaseState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
