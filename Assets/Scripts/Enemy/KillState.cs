using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyKillState: IState
{

    public WolfEnemy enemy;
    public Coroutine destroyCoroutine;
    public float shootDirection = 1.0f;

    public bool isShot;

    public EnemyKillState(WolfEnemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        if (!isShot) {
            float vx = enemy.transform.localScale.x < 0.0f ? 4.0f : -4.0f;
            enemy.rigidBody.velocity = new Vector3(vx, 3.0f, 0.0f);
            enemy.animator.SetBool("isDead", true);
        } else {
            enemy.rigidBody.velocity = new Vector3(4.0f * shootDirection, 3.0f, 0.0f);
            enemy.animator.SetBool("isSpoonDeath", true);
        }
    }
    public void Execute()
    {
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