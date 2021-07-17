using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerFightingState: IState
{
    PlayerMovement player;
    Coroutine punchCoroutine;

    int punchCount;

    public PlayerFightingState(PlayerMovement player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("Restarting");
        punchCount = 0;
        punchCoroutine = player.StartCoroutine(StopPunch());
    }
    public void Execute()
    {
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