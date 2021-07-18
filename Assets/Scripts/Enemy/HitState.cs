using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHitState: IState
{

    public Enemy enemy;
    public Player player;

    public EnemyHitState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public void Execute()
    {
       enemy.animator.SetBool("isHit", true);
    }

    public void Exit()
    {

    }
}