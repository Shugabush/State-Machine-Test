using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Player-Standard Jump", menuName = "Player Logic/Jump/Standard")]
public class PlayerStandardJump : PlayerJumpSOBase
{
    public override async void DoEnterLogic()
    {
        base.DoEnterLogic();
        entity.Anim.CrossFade("Jump", 0.125f);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
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
