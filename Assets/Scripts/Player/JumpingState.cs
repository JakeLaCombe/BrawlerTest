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
        Transform gameCollider = player.transform.Find("Floor Collider");
        gameCollider.gameObject.SetActive(false);
    }

    public void Execute()
    {
        float vx = 0;
        float vy =  player.rigidBody.velocity.y;
        float vz = 0;
        Transform gameCollider = player.transform.Find("Floor Collider");

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

        if (!player.input.Jump() && vy > 2.0f) {
            vy = 2.0f;
        }

        player.rigidBody.velocity = new Vector3(
           vx,
           vy,
           vz
        );

        player.animator.SetBool("isAirborne", true);

        if (vy < 0.0f) {
            gameCollider.gameObject.SetActive(vy < 0.0f);
        }

        if (Mathf.Abs(vy) <= 0.0001)
        {
            gameCollider.gameObject.SetActive(true);
            player.stateMachine.ChangeState(player.movingState);
        } 
    }

    public void Exit()
    {
        player.animator.SetBool("isRunning", true);
        player.animator.SetBool("isAirborne", false);
        Transform gameCollider = player.transform.Find("Floor Collider");
        gameCollider.gameObject.SetActive(true);
    }
}