using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyKnockBackState: IState
{

    public Enemy enemy;
    public Coroutine destroyCoroutine;

    public bool hitEffect = true;

    public EnemyKnockBackState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        float vx = enemy.transform.localScale.x < 0.0f ? -6.0f : 6.0f;
        enemy.rigidBody.velocity = new Vector3(vx, 3.0f, 0.0f);
        enemy.animator.SetBool("isDead", true);
    }
    public void Execute()
    {
       if (destroyCoroutine == null) {
         destroyCoroutine = enemy.StartCoroutine(StartWalk());
       }

       if (enemy.rigidBody.velocity.y == 0.0f)
       {
           enemy.StopCoroutine(destroyCoroutine);
           AdjustState();
       }
    }

    public void Exit()
    {
        enemy.animator.SetBool("isHit", false);
        enemy.animator.SetBool("isDead", false);
    }

    public IEnumerator StartWalk()
    {
        yield return new WaitForSeconds(1.0f);
        AdjustState();
    }

    private void AdjustState()
    {
         if (enemy.health > 0) {
            enemy.stateMachine.ChangeState(enemy.chaseState);
        } else {
            enemy.stateMachine.ChangeState(enemy.killState);
        }
    }
}