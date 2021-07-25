using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerKnockBackState: IState
{

    public Player player;
    public Coroutine destroyCoroutine;
    public Vector3 hitDirection;

    public bool hitEffect = true;

    public PlayerKnockBackState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.rigidBody.velocity = new Vector3(4.0f * hitDirection.x, 3.0f, 0.0f);
        player.animator.SetBool("isDead", true);
        player.animator.SetBool("isRunning", false);
    }
    public void Execute()
    {
       player.animator.SetBool("isDead", true);
       player.animator.SetBool("isRunning", false);
       
       if (destroyCoroutine == null) {
         destroyCoroutine = player.StartCoroutine(StartWalk());
       }

       if (player.rigidBody.velocity.y == 0.0f)
       {
           player.rigidBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
           player.StopCoroutine(destroyCoroutine);
           AdjustState();
       }
    }

    public void Exit()
    {
        player.animator.SetBool("isHit", false);
        player.animator.SetBool("isDead", false);

        if (destroyCoroutine == null) {
            player.StopCoroutine(destroyCoroutine);
        }

    }

    public IEnumerator StartWalk()
    {
        yield return new WaitForSeconds(1.0f);
        AdjustState();
    }

    private void AdjustState()
    {
         if (player.health > 0) {
            player.stateMachine.ChangeState(player.movingState);
        } else {
            player.stateMachine.ChangeState(player.killState);
        }
    }

    public void SetDirection(Vector3 hitDirection)
    {
        this.hitDirection = hitDirection;
    }
}