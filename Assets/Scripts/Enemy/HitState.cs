using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHitState: IState
{
    public Enemy enemy;
    public Coroutine recoverCoroutine;
    public int hitCount;
    public EnemyHitState(Enemy enemy)
    {
        this.enemy = enemy;
        hitCount = 0;
    }

    public void Enter()
    {
        enemy.animator.SetBool("isHit", true);
        hitCount = 0;
        enemy.transform.Find("EnemyHealth").gameObject.SetActive(true);
    }
    public void Execute()
    {
       enemy.animator.SetBool("isHit", true);
       enemy.transform.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);

       if (recoverCoroutine == null) {
         recoverCoroutine = enemy.StartCoroutine(recover());
       }

       if (enemy.health <= 0.0f)
       {
            enemy.stateMachine.ChangeState(enemy.killState);
       }

       SmallHealthBar healthBar = enemy.GetComponentInChildren<SmallHealthBar>();

       if (healthBar)
       {
            healthBar.SetHealthAmount(enemy.health / enemy.maxHealth);
       }
    }

    public void Exit()
    {
        enemy.animator.SetBool("isHit", false);       
        
        if (recoverCoroutine != null) {
            enemy.StopCoroutine(recoverCoroutine);
            recoverCoroutine = null;
        }

        enemy.transform.Find("EnemyHealth").gameObject.SetActive(false);
    }

    public IEnumerator recover()
    {
        yield return new WaitForSeconds(0.5f);
        recoverCoroutine = null;
        enemy.stateMachine.ChangeState(enemy.chaseState);
        hitCount = 0;
    }

    public void Increment()
    {
        if (hitCount >= 3) {
            enemy.stateMachine.ChangeState(enemy.knockBackState);
            return;
        }

        hitCount += 1;

        if (recoverCoroutine != null)
        {
            enemy.StopCoroutine(recoverCoroutine);
        }

        recoverCoroutine = enemy.StartCoroutine(recover());
    }
}
