using UnityEngine;

[CreateAssetMenu(fileName = "Player-Standard Move", menuName = "Player Logic/Move/Standard")]
public class PlayerStandardMove : PlayerMoveSOBase
{
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        entity.Anim.CrossFade("Walk", 0.05f);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        if (entity.MovementInput == Vector2.zero)
        {
            entity.StateMachine.ChangeState(entity.IdleState);
            return;
        }

        base.DoFrameUpdateLogic();
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
