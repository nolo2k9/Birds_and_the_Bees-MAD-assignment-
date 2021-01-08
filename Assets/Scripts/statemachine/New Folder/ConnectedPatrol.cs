using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectedPatrol : MonoBehaviour
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

    UnityEngine.AI.NavMeshAgent _navMeshAgent;

    ConnectedWayPoint _currentWaypoint;

    ConnectedWayPoint _previousWaypoint;

    bool _travelling;

    bool _waiting;

    int _wayPointsVisited;

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
            if (_currentWaypoint == null)
            {
                GameObject[] allwaypoints =
                    GameObject.FindGameObjectsWithTag("Waypoint");

                if (allwaypoints.Length > 0)
                {
                    while (_currentWaypoint == null)
                    {
                        int random =
                            UnityEngine.Random.Range(0, allwaypoints.Length);
                        ConnectedWayPoint startingWaypoint =
                            allwaypoints[random]
                                .GetComponent<ConnectedWayPoint>();

                        if (startingWaypoint != null)
                        {
                            _currentWaypoint = startingWaypoint;
                        }
                    }
                }
                else
                {
                    Debug.Log("Error");
                }
            }
            else
            {
                Debug.Log("No patrol points available");
            }
        }

        SetDestination();
    }

    // Update is called once per frame
    void Update()
    {
        //ch if bee is clse to destnation
        if (_travelling && _navMeshAgent.remainingDistance <= 1.0f)
        {
            _travelling = false;
            _wayPointsVisited++;

            //wait if wating
            if (_patrolWaiting)
            {
                _waiting = true;
                _waitTimer = 0f;
            }
            else
            {
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

               
                SetDestination();
            }
        }
    }

    private void SetDestination()
    {
        if (_wayPointsVisited > 0)
        {
            ConnectedWayPoint nextWayPoint =
                _currentWaypoint.NextWaypoint(_previousWaypoint);
            _previousWaypoint = _currentWaypoint;
            _currentWaypoint = nextWayPoint;
        }

        Vector3 targetVector = _currentWaypoint.transform.position;
        _navMeshAgent.SetDestination (targetVector);
        _travelling = true;
    }
}
