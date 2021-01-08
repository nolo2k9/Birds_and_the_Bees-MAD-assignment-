using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class States : MonoBehaviour
{   //enum of staes
    public enum STATE
    {
        IDLE,
        PATROL,
        CHASING,
        ATTACKING,
        SLEEP
    }
    //Events Enum 
    public enum EVENT
    {
        ENTER,
        UPDATE,
        EXIT
    }

    public STATE name;

    protected EVENT stage;

    protected GameObject npc;

    protected Transform bee;

    protected States nextState;

    protected NavMeshAgent agent;

    float visDist = 10.0f;

    float visAngle = 30.0f;

    float attack = 0.5f;

    public States(GameObject _npc, NavMeshAgent _agent, Transform _bee)
    {
        npc = _npc;
        agent = _agent;
        stage = EVENT.ENTER;
        bee = _bee;
    }

    public virtual void Enter()
    {
        stage = EVENT.UPDATE;
    }

    public virtual void Update()
    {
        stage = EVENT.UPDATE;
    }

    public virtual void Exit()
    {
        stage = EVENT.EXIT;
    }

    //runs from AI Controls state
    public States Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }

    //can npc see the bee
    public bool CanSeeBee()
    {
        Vector3 direction = bee.position - npc.transform.position;

        //must be facing to see bee
        float angle = Vector3.Angle(direction, npc.transform.forward);

        if (direction.magnitude < visDist && angle < visAngle)
        {
            //bee is visible
            return true;
        }
        return false;
    }

    public bool CanAttackBee()
    {   //move towards bees positioj
        Vector3 direction = bee.position - npc.transform.position;
        //can attack be in in range
        if (direction.magnitude < attack)
        {
            return true;
        }
        return false;
    }
}








