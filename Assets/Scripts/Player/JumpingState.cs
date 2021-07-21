using UnityEngine;
public class PlayerJumpingState: IState
{
    Player player;
    public PlayerJumpingState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    { 
        player.animator.SetBool("isRunning", false);
        player.animator.SetBool("isAirborne", true);
    }
    public void Execute()
    {
        float vx = 0;
        float vy =  player.rigidBody.velocity.y;
        float vz = 0;

        if (player.input.LeftHold()) {
            vx = -3.0f;
        } else if (player.input.RightHold()) {
            vx = 3.0f;
        }

        if (player.input.UpHold()) {
            vz = 3.0f;
        } else if (player.input.UpHold()) {
            vz = -3.0f;
        }

        if (player.input.Jump() && player.rigidBody.velocity.y <= Mathf.Abs(float.Epsilon)) {
            vy = 10.0f;
        }

        player.rigidBody.velocity = new Vector3(
           vx,
           vy,
           vz
        );

        if (Mathf.Abs(vy) < float.Epsilon)
        {
            player.stateMachine.ChangeState(player.movingState);
        }  
    }

    public void Exit()
    {
        player.animator.SetBool("isRunning", true);
        player.animator.SetBool("isAirborne", false);
    }
}