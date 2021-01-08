using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird2 : MonoBehaviour
{
    [SerializeField]
    Transform _destination;

    UnityEngine.AI.NavMeshAgent _navMeshAgent;

    void Start()
    {
        _navMeshAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();

        if (_navMeshAgent == null)
        {
            Debug.Log("Nav mesh agent is not attached");
        }
        else
        {
            SetDestination();
        }
    }

    private void SetDestination()
    {
        if (_destination != null)
        {
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination (targetVector);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
