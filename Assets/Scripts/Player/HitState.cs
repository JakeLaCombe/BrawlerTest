using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerHitState: IState
{

    public Player player;
    public int hitCount;
    public Vector3 hitDirection;
    public Coroutine recoverCoroutine;

    public bool hitEffect = true;

    public PlayerHitState(Player player)
    {
        this.player = player;
        hitCount = 0;
    }

    public void Enter()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.animator.SetBool("isHit", true);

        if (hitEffect) {
            hitEffect = false;
        }
    }
    public void Execute()
    {
       player.animator.SetBool("isHit", true);

       if (recoverCoroutine == null) {
         recoverCoroutine = player.StartCoroutine(recover());
       }

       if (player.health <= 0.0f)
       {
           player.stateMachine.ChangeState(player.killState);
       }
    }

    public void Exit()
    {
        player.animator.SetBool("isHit", false);       
        
        if (recoverCoroutine != null) {
            player.StopCoroutine(recoverCoroutine);
            recoverCoroutine = null;
        }

        hitEffect = true;
    }

    public IEnumerator recover()
    {
        player.health -= 1.0f;
        yield return new WaitForSeconds(1.0f);
        recoverCoroutine = null;
        player.stateMachine.ChangeState(player.movingState);
    }

    public void Increment()
    {
        if (hitCount >= 3) {
            Debug.Log("Switching to Knockback State");
            player.knockBackState.SetDirection(this.hitDirection);
            player.stateMachine.ChangeState(player.knockBackState);
            return;
        }

        Debug.Log("Uplifting Hits");

        hitCount += 1;

        if (recoverCoroutine != null)
        {
            player.StopCoroutine(recoverCoroutine);
        }

        recoverCoroutine = player.StartCoroutine(recover());
    }

    public void SetDirection(Vector3 hitDirection)
    {
        this.hitDirection = hitDirection;
    }
}