using UnityEngine;

public class State<TEntity> where TEntity : MonoBehaviour
{
    protected TEntity entity;
    protected StateMachine<TEntity> stateMachine;

    protected StateSOBase<Enemy> stateSO;

    public State(TEntity entity, StateMachine<TEntity> stateMachine, StateSOBase<Enemy> stateSO)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
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
