using UnityEngine;

public class State<TEntity> where TEntity : MonoBehaviour
{
    protected TEntity entity;

    protected StateSOBase<TEntity> stateSO;

    public State(TEntity entity, StateSOBase<TEntity> stateSO)
    {
        this.entity = entity;
        this.stateSO = stateSO;
    }

    public virtual void EnterState()
    {
        stateSO.DoEnterLogic();
    }
    public virtual void ExitState()
    {
        stateSO.DoExitLogic();
    }
    public virtual void FrameUpdate()
    {
        stateSO.DoFrameUpdateLogic();
    }
    public virtual void PhysicsUpdate()
    {
        stateSO.DoPhysicsLogic();
    }
}
