using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : States
{
    public Chase (GameObject _npc, UnityEngine.AI.NavMeshAgent _agent, Transform _bee) :
        base(_npc, _agent, _bee)
    {
        name = STATE.CHASING;
        agent.speed = 5;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update() {
        {
             Debug.Log("Chasing");
             //move towards bee
            agent.SetDestination(bee.position);
            if(agent.hasPath)
            {
                if(CanAttackBee())
                {   //call attack 
                    nextState = new Attack(npc, agent, bee);
                    //Set stage
                    stage = EVENT.EXIT;
                }
                else if(!CanSeeBee())
                {   //go back to patrolling
                    nextState = new Patrol(npc,agent,bee);
                    //Set stage
                    stage = EVENT.EXIT;
                }
            }
        }
    }

    public override void Exit() {
        {
            base.Exit();
        }
    }
}