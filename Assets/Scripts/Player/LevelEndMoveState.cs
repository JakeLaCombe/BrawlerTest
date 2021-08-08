using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndMoveState : IState
{
    // Start is called before the first frame update
    public Player player;
    public Coroutine destroyCoroutine;

    private Vector3 endTarget = new Vector3(0.0f, 0.0f, 0.0f);

    public bool hitEffect = true;

    public LevelEndMoveState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Vector3 targetVelocity = this.player.transform.position - this.endTarget;
        this.player.rigidBody.velocity = new Vector3(targetVelocity.x / 10.0f, 0.0f, targetVelocity.z / 10.0f);
    }
    public void Execute()
    {
        Vector3 targetVelocity = this.endTarget - this.player.transform.position;
        this.player.animator.SetBool("isRunning", true);
        this.player.rigidBody.velocity = new Vector3(targetVelocity.x * 2, 0.0f, targetVelocity.z * 2);

        if (Mathf.Abs(this.player.rigidBody.velocity.x) <= 1 && Mathf.Abs(this.player.rigidBody.velocity.z) < 1)
        {
            SceneManager.LoadScene("Level Two");
        }
    }

    public void Exit()
    {
        player.animator.SetBool("isAirborne", false);
    }

    public void SetTarget(Vector3 endTarget)
    {
        this.endTarget = endTarget;
    }
}
