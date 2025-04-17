using UnityEngine;

public class EnemyChaseState : State<Enemy>
{
    [SerializeField] float movementSpeed = 1.75f;

    public EnemyChaseState(Enemy entity, StateMachine<Enemy> stateMachine) : base(entity, stateMachine) { }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        if (entity.IsWithinStrikingDistance)
        {
            stateMachine.ChangeState(entity.AttackState);
            return;
        }
        if (!entity.IsAggroed)
        {
            stateMachine.ChangeState(entity.IdleState);
            return;
        }

        base.FrameUpdate();

        Vector3 moveDirection = (entity.playerTarget.position - entity.transform.position).normalized;

        entity.Move(moveDirection * movementSpeed);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
