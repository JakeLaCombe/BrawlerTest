using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHitState: IState
{

    public Enemy enemy;
    public Player player;
    public Coroutine recoverCoroutine;

    public bool hitEffect = true;

    public EnemyHitState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemy.animator.SetBool("isHit", true);

        if (hitEffect) {
            hitEffect = false;
        }
    }
    public void Execute()
    {
       enemy.animator.SetBool("isHit", true);

       if (recoverCoroutine == null) {
         recoverCoroutine = enemy.StartCoroutine(recover());
       }

       if (enemy.health <= 0.0f)
       {
           enemy.stateMachine.ChangeState(enemy.killState);
       }
    }

    public void Exit()
    {
        enemy.animator.SetBool("isHit", false);       
        
        if (recoverCoroutine != null) {
            enemy.StopCoroutine(recoverCoroutine);
            recoverCoroutine = null;
        }

        hitEffect = true;
    }

    public IEnumerator recover()
    {
        enemy.health -= 1.0f;
        yield return new WaitForSeconds(0.05f);
        recoverCoroutine = null;
        enemy.stateMachine.ChangeState(enemy.chaseState);
    }
}