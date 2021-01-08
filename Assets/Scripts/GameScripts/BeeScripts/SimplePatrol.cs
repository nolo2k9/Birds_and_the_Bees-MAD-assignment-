using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePatrol : MonoBehaviour
{
    //Controls if agent stays on flower
    [SerializeField]
    bool _patrolWaiting;

    //Time bee waits at flower
    [SerializeField]
    float _totalWaitTime = 10.0f;

    //Probability of switching direction
    [SerializeField]
    float _switchProbability = 0.2f;

    [SerializeField]
    List<Waypoint> _patrolPoints;

    //Behaviour variables
    UnityEngine.AI.NavMeshAgent _navMeshAgent;

    int _currentPatrolIndex;

    bool _travelling;

    bool _waiting;

    bool _patrolForward;

    float _waitTimer;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (_navMeshAgent == null)
        {
            Debug.Log("Nav mesh agent is not attached");
        }
        else
        {
            if (_patrolPoints != null && _patrolPoints.Count >= 2)
            {
                _currentPatrolIndex = 0;
                SetDestination();
            }
            else
            {
                Debug.Log("No patrol points available");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ch if bee is clse to destnation
        if (_travelling && _navMeshAgent.remainingDistance <= 1.0f)
        {
            _travelling = false;
        

            //wait if wating
            if (_patrolWaiting)
            {
                _waiting = true;
                _waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }
        }
        if (_waiting)
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= _totalWaitTime)
            {
                Debug.Log("Collecting Pollen");
                _waiting = false;

                ChangePatrolPoint();
                SetDestination();
            }
        }
    }

    private void SetDestination()
    {
        if (_patrolPoints != null)
        {
            Vector3 targetVector =
                _patrolPoints[_currentPatrolIndex].transform.position;
            _navMeshAgent.SetDestination (targetVector);
            _travelling = true;
        }
    }

    /*
        - Selects a new patrol point in the available list
        - also with a small probability allows us to move forwards or backwards
    */
    private void ChangePatrolPoint()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= _switchProbability)
        {
            _patrolForward = !_patrolForward;
        }
        if (_patrolForward)
        {
            _currentPatrolIndex =
                (_currentPatrolIndex + 1) % _patrolPoints.Count;
        }
        else
        {
            if (--_currentPatrolIndex < 0)
            {
                _currentPatrolIndex = _patrolPoints.Count - 1;
            }
        }
    }
}
