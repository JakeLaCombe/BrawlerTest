using UnityEngine;
public class PlayerMovingState: IState
{
    Player player;
    public PlayerMovingState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {

    }
    public void Execute()
    {
        float vx = 0;
        float vy =  player.rigidBody.velocity.y;
        float vz = 0;

        if (player.input.LeftHold()) {
            vx = -3.0f;
            player.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        } else if (player.input.RightHold()) {
            vx = 3.0f;
            player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        if (player.input.UpHold()) {
            vz = 3.0f;
        } else if (player.input.DownHold()) {
            vz = -3.0f;
        }

        if (player.input.Jump() &&  player.rigidBody.velocity.y <= Mathf.Abs(float.Epsilon)) {
            vy = 10.0f;
            player.stateMachine.ChangeState(player.jumpingState);
        }

        player.rigidBody.velocity = new Vector3(
           vx,
           vy,
           vz
        );

        if (player.input.AttackOne() && vy == 0)
        {
            player.stateMachine.ChangeState(player.fightingState);
        }

        player.animator.SetBool("isRunning", vx != 0 || vz != 0);
        player.animator.SetBool("isAirborne", false);
    }
    public void Exit()
    {

    }
}