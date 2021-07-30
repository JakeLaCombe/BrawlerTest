using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartState : IState
{
    // Start is called before the first frame update
     public Player player;
    public Coroutine destroyCoroutine;

    public bool hitEffect = true;

    public LevelStartState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.transform.position = new Vector3(player.transform.position.x, -20, player.transform.position.z);
        player.animator.SetBool("isAirborne", true);
    }
    public void Execute()
    {
        if (Mathf.Abs(player.rigidBody.velocity.y) <= 0.0001) {
            player.stateMachine.ChangeState(player.movingState);
        }
    }

    public void Exit()
    {
        player.animator.SetBool("isAirborne", false);
    }
}
