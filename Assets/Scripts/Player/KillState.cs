using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerKillState: IState
{
    public Player player;
    public Coroutine destroyCoroutine;

    public bool hitEffect = true;

    public PlayerKillState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        float vx = player.transform.localScale.x < 0.0f ? 6.0f : -6.0f;
        player.rigidBody.velocity = new Vector3(vx, 3.0f, 0.0f);
        player.animator.SetBool("isDead", true);
    }
    public void Execute()
    {
       player.animator.SetBool("isDead", true);
       player.animator.SetBool("isHit", true);

       if (destroyCoroutine == null) {
         destroyCoroutine = player.StartCoroutine(Revive());
       }
    }

    public void Exit()
    {
        player.animator.SetBool("isDead", false);
        player.animator.SetBool("isHit", false);
    }

    public IEnumerator Revive()
    {
        yield return new WaitForSeconds(3.0f);
        player.Revive();
    }
}