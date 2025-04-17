using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Straight Single Projectile", menuName = "Enemy Logic/Attack Logic/Straight Single Projectile")]
public class EnemyAttackSingleStraightProjectile : EnemyAttackSOBase
{
    [SerializeField] Timer shotTimer = new Timer(2f);
    [SerializeField] Timer exitTimer = new Timer(3f);

    [SerializeField] float bulletSpeed = 10f;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        shotTimer.Reset();
        exitTimer.Reset();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        if (!entity.IsWithinStrikingDistance)
        {
            exitTimer.Update(Time.deltaTime);
            if (exitTimer.Laps >= 1)
            {
                entity.StateMachine.ChangeState(entity.ChaseState);
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
