using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHitState: IState
{
    public Enemy enemy;
    public Player player;
    public Coroutine recoverCoroutine;
    public int hitCount;
    public bool hitEffect = true;

    public EnemyHitState(Enemy enemy)
    {
        this.enemy = enemy;
        hitCount = 0;
    }

    public void Enter()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemy.animator.SetBool("isHit", true);
        hitCount = 0;

        if (hitEffect) {
            hitEffect = false;
        }

        enemy.transform.Find("EnemyHealth").gameObject.SetActive(true);
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

        hitEffect = true;
        enemy.transform.Find("EnemyHealth").gameObject.SetActive(false);
    }

    public IEnumerator recover()
    {
        yield return new WaitForSeconds(0.2f);
        recoverCoroutine = null;
        enemy.stateMachine.ChangeState(enemy.chaseState);

        hitEffect = true;
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
