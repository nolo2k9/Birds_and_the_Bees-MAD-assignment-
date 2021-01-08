using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[
    RequireComponent(
        typeof (UnityEngine.AI.NavMeshAgent),
        typeof (FiniteStateMachine))
]
public class Bird : MonoBehaviour
{
    [SerializeField]
    ConnectedWayPoint[] _patrolPoints;

    UnityEngine.AI.NavMeshAgent _navMeshAgent;

    FiniteStateMachine _finiteMachine;

    public void Awake()
    {
        _navMeshAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        _finiteMachine = this.GetComponent<FiniteStateMachine>();
    }

    public void Start()
    {
    }

    public void Update()
    {
    }

    public ConnectedWayPoint[] PatrolPoints
    {
        get
        {
            return _patrolPoints;
        }
    }
}
