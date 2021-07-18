using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerFightingState: IState
{
    Player player;
    Coroutine punchCoroutine;

    int punchCount;

    public PlayerFightingState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        punchCount = 0;
        punchCoroutine = player.StartCoroutine(StopPunch());
    }
    public void Execute()
    {
        player.rigidBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);

        if(player.input.AttackOne())
        {
            if (punchCoroutine != null)
            {
                player.StopCoroutine(punchCoroutine);
            }

            punchCoroutine = player.StartCoroutine(StopPunch());
            punchCount++;
        }

        if (punchCount % 2 == 1) {
            player.animator.SetBool("isPunching", true);
            player.animator.SetBool("isPunchingTwo", false);
        } else {
            player.animator.SetBool("isPunching", false);
            player.animator.SetBool("isPunchingTwo", true);
        }

        if (player.GetHitTarget())
        {
            GameObject.Destroy(player.GetHitTarget());
            player.SetHitTarget(null);
        }
    }
    public void Exit()
    {

    }

    private IEnumerator StopPunch()
    {                
        yield return new WaitForSeconds(0.2f);
        player.animator.SetBool("isPunching", false);
        player.animator.SetBool("isPunchingTwo", false);
        player.stateMachine.ChangeState(player.movingState);
    }
    
}