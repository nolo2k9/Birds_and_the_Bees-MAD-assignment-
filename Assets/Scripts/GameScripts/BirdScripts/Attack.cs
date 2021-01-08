using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : States
{
    float rotationSpeed = 2.0f;
    public Attack(GameObject _npc, UnityEngine.AI.NavMeshAgent _agent, Transform _bee) :
        base(_npc, _agent, _bee)
    {
        name = STATE.ATTACKING;
        
    }


    public override void Enter() {
        {
            agent.isStopped = true;
            base.Enter();
        }
    }

    public override void Update() {
        {
            Debug.Log("Attacking");
            void OnCollisionEnter(Collision other){
            if(other.gameObject.tag =="Bee"){
                Destroy(gameObject);
            }
            
     }      //Must be able to see the bee to attack the bee
            Vector3 direction = bee.position - npc.transform.position;
            float angle = Vector3.Angle(direction, npc.transform.forward);
            direction.y =0;
            //face bee while attacking
            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,
            Quaternion.LookRotation(direction),
            Time.deltaTime * rotationSpeed);

            if(!CanAttackBee())
            {   //go back to idle
                nextState = new Idle(npc, agent, bee);
                stage = EVENT.EXIT;
            }
        }
    }

    public override void Exit() {
        {
            base.Exit();
        }
    }

}
