using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    [SerializeField]
    AbstractFSMState _startingState;

    AbstractFSMState _currentState;

    [SerializeField]
    List<AbstractFSMState> _validStates;

    Dictionary<FSMStateType, AbstractFSMState> _fsmStates;

    public void Awake()
    {
        _currentState = null;
        _fsmStates = new Dictionary<FSMStateType, AbstractFSMState>();
        UnityEngine.AI.NavMeshAgent navMeshAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        Bird bird = this.GetComponent<Bird>();
        foreach (AbstractFSMState state in _validStates)
        {
            state.SetExecutingFSM(this);
            state.SetExecutingNPC (bird);
            state.SetNavMeshAgent (navMeshAgent);
            _fsmStates.Add(state.StateType, state);
        }
    }

    public void Start()
    {
        if (_startingState != null)
        {
            EnterState (_startingState);
        }
    }

    public void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState();
        }
    }


#region STATE MANAGEMENT

    public void EnterState(AbstractFSMState nextState)
    {
        if (nextState == null)
        {
            return;
        }
        if (_currentState != null)
        {
            _currentState.ExitState();
            
        }
        _currentState = nextState;
            _currentState.EnterState();
    }

    public void EnterState(FSMStateType stateType)
    {
        if (_fsmStates.ContainsKey(stateType))
        {
            AbstractFSMState nextState = _fsmStates[stateType];

            EnterState (nextState);
        }
    }


#endregion
}
