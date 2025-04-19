using UnityEngine;

[CreateAssetMenu(fileName = "Player-Standard Idle", menuName = "Player Logic/Idle/Standard")]
public class PlayerStandardIdle : PlayerIdleSOBase
{
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        entity.Anim.CrossFade("Idle", 0.125f);
        var clips = entity.Anim.GetCurrentAnimatorClipInfo(0);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        if (entity.MovementInput != Vector2.zero)
        {
            
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
