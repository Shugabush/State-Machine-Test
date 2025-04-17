using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Random Wander", menuName = "Enemy Logic/Idle Logic/Random Wander")]
public class EnemyIdleRandomWander : EnemyIdleSOBase
{
    public float randomMovementRange = 1f;
    public float randomMovementSpeed = 1f;

    Vector3 targetPos;
    Vector3 direction;

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
        if (entity.IsAggroed)
        {
            entity.StateMachine.ChangeState(entity.ChaseState);
            return;
        }

        base.DoFrameUpdateLogic();

        direction = (targetPos - entity.transform.position).normalized;

        entity.Move(direction * randomMovementSpeed);

        if ((entity.transform.position - targetPos).sqrMagnitude < 0.01f)
        {
            targetPos = GetRandomPointInCircle();
        }
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

    Vector3 GetRandomPointInCircle()
    {
        return entity.transform.position + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)) * randomMovementRange;
    }
}
