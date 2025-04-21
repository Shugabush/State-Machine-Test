using UnityEngine;

[CreateAssetMenu(fileName = "Player-Standard Fall", menuName = "Player Logic/Fall/Standard")]
public class PlayerStandardFall : PlayerFallSOBase
{
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        entity.Anim.CrossFade("Fall", 0.05f);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        if (entity.IsGrounded)
        {
            entity.StateMachine.ChangeState(entity.MovementInput == Vector2.zero ? entity.IdleState : entity.MoveState);
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(Player entity)
    {
        base.Initialize(entity);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
