using UnityEngine;

public class State<TEntity> where TEntity : MonoBehaviour
{
    protected TEntity entity;
    protected StateMachine<TEntity> stateMachine;

    public State(TEntity entity, StateMachine<TEntity> stateMachine)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
}
