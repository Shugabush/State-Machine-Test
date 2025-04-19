using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Direct Chase", menuName = "Enemy Logic/Chase Logic/Direct Chase")]
public class EnemyChaseDirectToPlayer : EnemyChaseSOBase
{
    [SerializeField] float movementSpeed = 1.75f;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        if (entity.IsWithinStrikingDistance)
        {
            entity.StateMachine.ChangeState(entity.AttackState);
            return;
        }
        if (!entity.IsAggroed)
        {
            entity.StateMachine.ChangeState(entity.IdleState);
            return;
        }

        base.DoFrameUpdateLogic();

        Vector3 moveDirection = (entity.playerTarget.position - entity.transform.position).normalized;

        entity.Move(moveDirection * movementSpeed);
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(Enemy entity)
    {
        base.Initialize(entity);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
