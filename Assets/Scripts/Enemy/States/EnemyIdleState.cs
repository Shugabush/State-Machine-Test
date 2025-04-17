using UnityEngine;

public class EnemyIdleState : State<Enemy>
{
    Vector3 targetPos;
    Vector3 direction;

    public EnemyIdleState(Enemy entity, StateMachine<Enemy> stateMachine) : base(entity, stateMachine) { }

    public override void EnterState()
    {
        base.EnterState();

        targetPos = GetRandomPointInCircle();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        if (entity.IsAggroed)
        {
            stateMachine.ChangeState(entity.ChaseState);
            return;
        }

        base.FrameUpdate();

        direction = (targetPos - entity.transform.position).normalized;

        entity.Move(direction * entity.randomMovementSpeed);

        if ((entity.transform.position - targetPos).sqrMagnitude < 0.01f)
        {
            targetPos = GetRandomPointInCircle();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    Vector3 GetRandomPointInCircle()
    {
        return entity.transform.position + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)) * entity.randomMovementRange;
    }
}
