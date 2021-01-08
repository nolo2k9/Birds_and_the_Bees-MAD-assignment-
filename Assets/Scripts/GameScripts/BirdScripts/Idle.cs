using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : States
{
    //constructor
    public Idle(GameObject _npc, UnityEngine.AI.NavMeshAgent _agent, Transform _bee) :
        base(_npc, _agent, _bee)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Idle");
    }

    public override void Update()
    {
        Debug.Log("Idle");
        if(CanSeeBee())
        {   //Start a chase
            nextState = new Chase(npc, agent, bee);
            //Set stage
            stage = EVENT.EXIT;
            //chances a patrol will start 10%
        } else if (Random.Range(0, 100) < 10) {
            //start patrolling
            nextState = new Patrol(npc, agent, bee);
            stage = EVENT.EXIT;
        }
        
    }

    public override void Exit()
    {
        base.Exit();
    }
}