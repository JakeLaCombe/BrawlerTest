using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyChaseState: IState
{

    public Enemy enemy;
    public Player player;

    public EnemyChaseState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public void Execute()
    {
        float vx = 0;
        float vy = enemy.rigidBody.velocity.y;
        float vz = 0;
        
        Vector3 target = player.transform.position;
        Vector3 currentPosition = enemy.transform.position;

        if (target.x + 1.5f < currentPosition.x) {
            vx = -2.0f;
        } else if (target.x - 1.5f > currentPosition.x) {
            vx = 2.0f;
        }

        if (target.z + 0.1f < currentPosition.z) {
            vz = -2.0f;
        } else if (target.z -0.1f > currentPosition.z) {
            vz = 2.0f;
        }

        enemy.rigidBody.velocity = new Vector3(vx, vy, vz);

        if (vx < 0) {
            enemy.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        } else if (vx > 0) {
            enemy.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        enemy.animator.SetBool("isRunning", enemy.rigidBody.velocity.x != 0.0f || enemy.rigidBody.velocity.z != 0.0f);
    }

    public void Exit()
    {

    }
}