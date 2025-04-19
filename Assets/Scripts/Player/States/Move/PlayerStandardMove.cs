using UnityEngine;

[CreateAssetMenu(fileName = "Player-Standard Move", menuName = "Player Logic/Move/Standard")]
public class PlayerStandardMove : PlayerMoveSOBase
{
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        entity.Anim.CrossFade("Walk", 0.125f);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        entity.MoveWithInput();
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
