using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerKnockBackState: IState
{

    public Player player;
    public Coroutine destroyCoroutine;

    public bool hitEffect = true;

    public PlayerKnockBackState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        float vx = player.transform.localScale.x < 0.0f ? 6.0f : -6.0f;
        player.rigidBody.velocity = new Vector3(vx, 3.0f, 0.0f);
        player.animator.SetBool("isDead", true);
        player.animator.SetBool("isRunning", false);
    }
    public void Execute()
    {
       if (destroyCoroutine == null) {
         destroyCoroutine = player.StartCoroutine(StartWalk());
       }

       if (player.rigidBody.velocity.y == 0.0f)
       {
           player.StopCoroutine(destroyCoroutine);
           AdjustState();
       }
    }

    public void Exit()
    {
        player.animator.SetBool("isHit", false);
        player.animator.SetBool("isDead", false);
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
}