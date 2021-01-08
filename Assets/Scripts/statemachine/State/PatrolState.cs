using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[
    CreateAssetMenu(
        fileName = "PatrolState",
        menuName = "Unity-FSM/states/Patrol",
        order = 2)
]
public class PatrolState : AbstractFSMState
{
    ConnectedWayPoint[] _patrolPoints;

    int _patrolPointIndex;

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.PATROL;
        _patrolPointIndex = -1;
    }

    public override bool EnterState()
    {
        EnteredState = false;
        if (base.EnterState())
        {
            //Get patrol points
            _patrolPoints = _bird.PatrolPoints;

            if (_patrolPoints == null || _patrolPoints.Length == 0)
            {
                Debug.LogError("Patrol state error");
            }
            else
            {
                if (_patrolPointIndex < 0)
                {
                    _patrolPointIndex =
                        UnityEngine.Random.Range(0, _patrolPoints.Length);
                }
                else
                {
                    _patrolPointIndex =
                        (_patrolPointIndex + 1) % _patrolPoints.Length;
                }
                SetDestination(_patrolPoints[_patrolPointIndex]);
                EnteredState = true;
            }
        }

        return EnteredState;
    }

    public override void UpdateState()
    {
        //TODO make sure sucessfully entered state
        if(EnteredState)
        {
            if(Vector3.Distance(_navMeshAgent.transform.position, _patrolPoints[_patrolPointIndex].transform.position) <=1f)
            {
                _fsm.EnterState(FSMStateType.IDLE);
            }
        }
        Debug.Log("Updating Patrol state");
    }

    private void SetDestination(ConnectedWayPoint destination)
    {
        if (_navMeshAgent != null && destination != null)
        {
            _navMeshAgent.SetDestination(destination.transform.position);
        }
    }
}
