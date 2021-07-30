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
        player.animator.SetBool("isAirborne", false);
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
            vy = 12.0f;
            player.stateMachine.ChangeState(player.jumpingState);
        }

        player.rigidBody.velocity = new Vector3(
           vx,
           vy,
           vz
        );

        bool isFightable = Mathf.Abs(vy) <= 0.0001;

        if (player.input.AttackOne() && isFightable)
        {
            player.stateMachine.ChangeState(player.fightingState);
        }

        if (player.input.Throw() && isFightable && player.silverCount > 0)
        {
            player.animator.SetBool("isPunching", true);
            Spoon spoon = GameObject.Instantiate(PrefabsManager.instance.spoon, player.attackZone.transform.position + new Vector3(0.0f, 0.25f, 0.0f), Quaternion.identity);
            spoon.StartThrow(player.transform.localScale.x);
            player.silverCount -= 1;
        }
        else
        {
            player.animator.SetBool("isPunching", false);
        }

        player.animator.SetBool("isRunning", vx != 0 || vz != 0);
        player.animator.SetBool("isAirborne", false);
    }
    public void Exit()
    {

    }
}