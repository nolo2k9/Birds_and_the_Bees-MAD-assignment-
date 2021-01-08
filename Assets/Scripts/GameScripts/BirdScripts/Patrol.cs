using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : States
{
    int currentIndex = -1;
    
    public Patrol(GameObject _npc, UnityEngine.AI.NavMeshAgent _agent, Transform _bee) :
        base(_npc, _agent, _bee)
    {
        name = STATE.PATROL;
        agent.speed = 2;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        Debug.Log("Patrolling");
        //Nothing bigger than infinity
        float lastDistance = Mathf.Infinity;
        //loop over checkpoints
        for(int i = 0; i < GameEnvironment.Singelton.CheckPoints.Count; i++)
        {   //After chasing go back to the last way point 
            GameObject thisWP = GameEnvironment.Singelton.CheckPoints[i];
            float distance = Vector3.Distance(npc.transform.position, thisWP.transform.position);
            if(distance < lastDistance)
            {
                currentIndex = i-1;
                lastDistance = distance;
            }
        }
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        Debug.Log("Patrolling");
        //if distance left is greater than 1
        if (agent.remainingDistance < 1)
        {
            //Decrement 
            if (currentIndex >= GameEnvironment.Singelton.CheckPoints.Count - 1)
                currentIndex = 0;
            else
                currentIndex++;
                //move to new position
            agent
                .SetDestination(GameEnvironment
                    .Singelton
                    .CheckPoints[currentIndex]
                    .transform
                    .position);
        }
        //chase bee if possible
        if (CanSeeBee())
        {
            nextState = new Chase(npc, agent, bee);
            stage = EVENT.EXIT;
        }
    }
}
