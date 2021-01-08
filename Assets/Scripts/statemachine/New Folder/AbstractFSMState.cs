using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExecutionState
{
    NONE,
    ACTIVE,
    COMPLETED,
    TERINATED
}

public enum FSMStateType
{
    IDLE,
    PATROL,

}

public abstract class AbstractFSMState : ScriptableObject
{
    public ExecutionState ExecutionState { get; protected set; }
    public FSMStateType StateType{get; protected set;}
    public bool EnteredState {get; protected set;}

    protected UnityEngine.AI.NavMeshAgent _navMeshAgent;

    protected Bird _bird;

    protected FiniteStateMachine _fsm;

    public virtual void OnEnable()
    {
        ExecutionState = ExecutionState.NONE;
    }

    public virtual bool EnterState()
    {
        bool successNavMesh = true;
        bool successNPC = true;
        ExecutionState = ExecutionState.ACTIVE;

        //Does the navmesh agent exist
        successNavMesh = (_navMeshAgent != null);

        //Does NPC exist?
        successNPC = (_bird != null);

        return successNavMesh & successNPC;
    } //used when entering a state

    public virtual bool ExitState()
    {
        ExecutionState = ExecutionState.COMPLETED;
        return true;
    } //used when exiting a state

    public abstract void UpdateState();

    public virtual void SetNavMeshAgent(
        UnityEngine.AI.NavMeshAgent navMeshAgent
    )
    {
        if (navMeshAgent != null)
        {
            _navMeshAgent = navMeshAgent;
        }
    }

    public virtual void SetExecutingFSM(FiniteStateMachine fsm){
        if(fsm!=null)
        {
            _fsm = fsm;
        }
    }

    public virtual void SetExecutingNPC(Bird bird)
    {
        if (bird != null)
        {
            _bird = bird;
        }
    }
}
