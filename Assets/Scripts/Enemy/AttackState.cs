using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState
{
    // Start is called before the first frame update

    public WolfEnemy enemy;
    public int hitCount = 3;
    public List<int> attackChain;
    public Coroutine punchCoroutine;

    public EnemyAttackState(WolfEnemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        attackChain = new List<int>(new int[]{1, 2});
        punchCoroutine = this.enemy.StartCoroutine(switchAttack());
    }
    public void Execute()
    {
        this.enemy.animator.SetBool("isAttacking", true);

        if (this.enemy.getHitTarget() == null)
        {
            if (punchCoroutine != null)
            {
                enemy.StopCoroutine(punchCoroutine);
            }

            enemy.stateMachine.ChangeState(enemy.chaseState);
        }
    }

    public void Exit()
    {
        this.enemy.animator.SetBool("isAttacking", false);

        if (punchCoroutine != null)
        {
            enemy.StopCoroutine(punchCoroutine);
        }
    }

    public IEnumerator switchAttack()
    {
        yield return new WaitForSeconds(0.5f);
        SoundManager.instance.Punch.Play();
        this.enemy.animator.SetInteger("attackType", attackChain[0]);
        attackChain.RemoveAt(0);
        this.enemy.animator.SetBool("isAttacking", attackChain.Count == 0);

        if (this.enemy.getHitTarget())
        {
            Player player = this.enemy.getHitTarget().GetComponent<Player>();
            Vector3 direction = new Vector3(this.enemy.transform.localScale.x * -1.0f, 1.0f, 1.0f);
            player.Hit(direction);
        }

        if (attackChain.Count <= 0)
        {
            this.enemy.stateMachine.ChangeState(this.enemy.chaseState);
        }
        else
        {
            punchCoroutine = this.enemy.StartCoroutine(switchAttack());
        }
    }
}
