using UnityEngine;

public class EnemyAttackState : State<Enemy>
{
    [SerializeField] Timer shotTimer = new Timer(2f);
    [SerializeField] Timer exitTimer = new Timer(3f);

    [SerializeField] float bulletSpeed = 10f;

    public EnemyAttackState(Enemy entity, StateMachine<Enemy> stateMachine) : base(entity, stateMachine) { }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
        shotTimer.Reset();
        exitTimer.Reset();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (!entity.IsWithinStrikingDistance)
        {
            exitTimer.Update(Time.deltaTime);
            if (exitTimer.Laps >= 1)
            {
                stateMachine.ChangeState(entity.ChaseState);
            }
            return;
        }
        exitTimer.Reset();

        shotTimer.Update(Time.deltaTime);

        entity.Move(Vector3.zero);

        if (shotTimer.Laps >= 1)
        {
            shotTimer.Reset();

            Vector3 dir = (entity.playerTarget.position - entity.transform.position).normalized;
            Bullet bullet = Object.Instantiate(entity.BulletPrefab, entity.transform.position, Quaternion.identity);
            bullet.Fire(dir * bulletSpeed);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
