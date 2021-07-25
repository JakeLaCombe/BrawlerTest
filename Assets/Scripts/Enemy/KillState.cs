using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyKillState: IState
{

    public WolfEnemy enemy;
    public Coroutine destroyCoroutine;

    public bool hitEffect = true;

    public EnemyKillState(WolfEnemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        float vx = enemy.transform.localScale.x < 0.0f ? 6.0f : -6.0f;
        enemy.rigidBody.velocity = new Vector3(vx, 3.0f, 0.0f);
        enemy.animator.SetBool("isDead", true);
    }
    public void Execute()
    {
       enemy.animator.SetBool("isHit", true);

       if (destroyCoroutine == null) {
         destroyCoroutine = enemy.StartCoroutine(DestroyObject());
       }
    }

    public void Exit()
    {
    }

    public IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(3.0f);
        GameObject.Destroy(enemy.gameObject);
    }
}