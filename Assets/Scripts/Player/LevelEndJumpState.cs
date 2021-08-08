using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndJumpState : IState
{
    // Start is called before the first frame update
     public Player player;
    public Coroutine destroyCoroutine;

    public bool hitEffect = true;

    public LevelEndJumpState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        this.player.rigidBody.velocity = new Vector3(0.0f, 15.0f, 0.0f);
    }
    public void Execute()
    {
        if (!player.spriteRenderer.isVisible) {
            SceneManager.LoadScene("Title Scene");
        }
    }

    public void Exit()
    {
        player.animator.SetBool("isAirborne", false);
    }
}
