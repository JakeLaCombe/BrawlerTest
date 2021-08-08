using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyChaseState: IState
{

    public WolfEnemy enemy;
    public Player player;

    public EnemyChaseState(WolfEnemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public void Execute()
    {
        if (!player.isPlayerControlled()) {
            return;
        }

        float vx = enemy.rigidBody.velocity.x;
        float vy = enemy.rigidBody.velocity.y;
        float vz = enemy.rigidBody.velocity.z;

        if (player != null && Mathf.Abs(player.transform.position.x - enemy.transform.position.x) < 20) {
            Vector3 target = player.transform.position;
            Vector3 currentPosition = enemy.transform.position;

            if (target.x + 1.25f < currentPosition.x) {
                vx = -2.0f;
            } else if (target.x - 1.25f > currentPosition.x) {
                vx = 2.0f;
            }

            if (target.z + 0.1f < currentPosition.z) {
                vz = -2.0f;
            } else if (target.z -0.1f > currentPosition.z) {
                vz = 2.0f;
            }
        }

        if (vx < 0) {
            enemy.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        } else if (vx > 0) {
            enemy.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }

        if (enemy.floorDetectorHorizontal.GetFloor() == "")
        {
            Debug.Log("Nothing To Move"); 
            vx = 0;
        }

        if (enemy.floorDetectorUp.GetFloor() == "" && vz > 0.0f)
        {
            vz = 0;
        }

        if (enemy.floorDetectorDown.GetFloor() == "" && vz < 0.0f)
        {
            vz = 0;
        }

        enemy.rigidBody.velocity = new Vector3(vx, vy, vz);

        enemy.animator.SetBool("isRunning", enemy.rigidBody.velocity.x != 0.0f || enemy.rigidBody.velocity.z != 0.0f);

        if (enemy.getHitTarget() != null)
        {
            enemy.AttemptAttack();
        }
    }

    public void Exit()
    {

    }
}